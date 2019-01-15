using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameWebMVC.Areas.Admin.Controllers
{
    public class CharactersController : Controller
    {
        #region Constants

        public const int PageSize = 20;

        #endregion Constants

        #region Facades

        public CharacterFacade CharacterFacade { get; set; }

        #endregion Facades

        public ActionResult Index()
        {
            return RedirectToAction("List", "Characters", new { area = "Admin" });
        }

        public async Task<ActionResult> List(int page = 1)
        {
            var filter = new CharacterFilterDto { SortCriteria = nameof(CharacterDto.Score), PageSize = PageSize, RequestedPageNumber = page };
            var result = await CharacterFacade.GetCharactersByFilterAsync(filter);

            // Paging
            ViewBag.RequestedPageNumber = result.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)result.TotalItemsCount / (double)PageSize);
            // Paging END

            return View("List", result.Items);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await CharacterFacade.GetCharacterById(id);
            return View("Details", model);
        }

        #region Edit

        public async Task<ActionResult> Edit()
        {
            var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            return View(character);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CharacterDto characterDto)
        {
            try
            {
                var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
                character.Name = characterDto.Name;
                var res = await CharacterFacade.EditCharacter(character);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion Edit

        public async Task<ActionResult> Remove(Guid id)
        {
            await CharacterFacade.RemoveCharacter(id);
            return RedirectToAction("List");
        }
    }
}