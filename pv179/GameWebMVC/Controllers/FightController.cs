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

    [Authorize(Roles = "HasCharacter")]
    public class FightController : Controller
    {

        #region Constants

        public const int PageSize = 10;

        #endregion Constants

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
            var characterId = Guid.Parse(User.Identity.Name);
            if (characterId != fight.AttackerId && characterId != fight.DefenderId)
            {
                return RedirectToAction("NotAuthorized", "Error");
            }
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

        public async Task<ActionResult> List(int page = 1)
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var fights = await CharacterFacade.GetFightsHistory(new FightFilterDto { FighterId = characterId, PageSize = PageSize, RequestedPageNumber = page, SortCriteria = nameof(FightDto.Timestamp) });

            // Paging
            ViewBag.RequestedPageNumber = fights.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)fights.TotalItemsCount / (double)PageSize);
            // Paging END
            return View(fights.Items);

        }
    }
}