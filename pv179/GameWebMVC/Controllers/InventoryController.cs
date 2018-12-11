using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    [Authorize(Roles = "HasCharacter")]
    public class InventoryController : Controller
    {

        public CharacterFacade CharacterFacade { get; set; }


        public async Task<ActionResult> Index()
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var character = await CharacterFacade.GetCharacterById(characterId);
            var items = await CharacterFacade.GetItemsByFilterAsync(new ItemFilterDto { OwnerId = characterId });
            var model = new InventoryModel { Money = character.Money, Items = items.Items };
            if(character.Money < 500)
            {
                ModelState.AddModelError("Money", "nemáš dostatok peňazí");
            }
            return View(model);
        }


        public async Task<ActionResult> Details(Guid id)
        {
            var item = await CharacterFacade.GetItem(id);
            return View(item);
        }



        public async Task<ActionResult> Buy()
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var res = await CharacterFacade.BuyItemAsync(characterId);
            return RedirectToAction("Index");

  
        }


        public async Task<ActionResult> Sell(Guid id)
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var item = await CharacterFacade.GetItem(id);
            if(item.OwnerId == characterId)
            {
                await CharacterFacade.SellItem(id);
            }
            else
            {
                return View("Error");
            }

            return RedirectToAction("Index");

        }



        public async Task<ActionResult> Equip(Guid id)
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var succ = await CharacterFacade.EquipItemAsync(characterId, id);
            if (succ)
            {
                return RedirectToAction("Index");

            } else
            {
                return View("Error");
            }
            
        }

    }
}
