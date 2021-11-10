using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    [Authorize(Roles = "HasCharacter")]
    public class InventoryController : Controller
    {
        #region Constants

        public const int PageSize = 10;

        #endregion Constants

        public CharacterFacade CharacterFacade { get; set; }

        public async Task<ActionResult> Index(int page = 1)
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var character = await CharacterFacade.GetCharacterById(characterId);
            var items = await CharacterFacade.GetItemsByFilterAsync(new ItemFilterDto { OwnerId = characterId, PageSize = PageSize, RequestedPageNumber = page });
            var model = new InventoryModel { Money = character.Money, Items = items.Items };

            // Paging
            ViewBag.RequestedPageNumber = items.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)items.TotalItemsCount / (double)PageSize);
            // Paging END
            return View(model);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var item = await CharacterFacade.GetItem(id);
            return View(item);
        }

        public async Task<ActionResult> Shop()
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var character = await CharacterFacade.GetCharacterById(characterId);
            var items = CharacterFacade.GetItemsForShop();
            var model = new InventoryModel { Money = character.Money, Items = items };
            return View(model);
        }

        public async Task<ActionResult> Buy(ItemDto item)
        {
            var characterId = Guid.Parse(User.Identity.Name);

            var res = await CharacterFacade.BuyItemAsync(characterId, item);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Sell(Guid id)
        {
            var characterId = Guid.Parse(User.Identity.Name);
            var item = await CharacterFacade.GetItem(id);
            if (item.OwnerId == characterId)
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
            }
            else
            {
                return View("Error");
            }
        }
    }
}