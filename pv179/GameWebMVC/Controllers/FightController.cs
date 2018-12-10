using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    [Authorize]
    public class FightController : Controller
    {
        public CharacterFacade CharacterFacade { get; set; }

        public async Task<ActionResult> FightSelection()
        {
            try
            {
                var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
                var fighters = await CharacterFacade.GetCharactersToFight(character, 10);
                return View(fighters);
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Index(Guid id)
        {
            var fight = await CharacterFacade.GetFight(id);
            return View(fight);
        }

        public async Task<ActionResult> Create(Guid id)
        {
            try
            {
                var fightId = await CharacterFacade.Attack(Guid.Parse(User.Identity.Name), id);
                return RedirectToAction("Index", new { id = fightId });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> List()
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var character = await CharacterFacade.GetCharacterById(characterId);
            var fights = new List<FightDto>();
            var attackerFights = await CharacterFacade.GetFightsHistory(new FightFilterDto { AttackerId = characterId });
            var defenderFights = await CharacterFacade.GetFightsHistory(new FightFilterDto { DefenderId = characterId });
            fights.AddRange(attackerFights.Items);
            fights.AddRange(defenderFights.Items);
            fights = fights.OrderByDescending(x => x.Timestamp).ToList();

            return View(fights);

        }
    }
}