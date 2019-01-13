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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCharacter character)
        {
            var newGroupId = await CharacterFacade.CreateCharacter(character.AccountId, character.Character);
            return RedirectToAction("Details", new { area = "Admin", id = character.Character.Id });
        }

        /*public async Task<ActionResult> Edit(Guid id)
        {
            var group = await GroupFacade.GetGroupAsync(id);
            return View("Edit", new GroupImageModel { Group = group, File = null });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GroupImageModel model)
        {
            try
            {
                var group = await GroupFacade.GetGroupAsync(model.Group.Id);
                var relativePath = group.Picture;
                if (model.File != null && model.File.ContentLength > 0)
                {
                    var fileType = Path.GetExtension(model.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img/"), model.Group.Id + fileType);
                    model.File.SaveAs(path);
                    relativePath = "/Img/" + model.Group.Id + fileType;
                    model.Group.Picture = relativePath;
                }
                model.Group.Picture = relativePath;
                await GroupFacade.Edit(model.Group);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR: " + ex.Message.ToString();
            }
            return RedirectToAction("Details", new { area = "Admin", id = model.Group.Id });
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            await GroupFacade.RemoveGroup(id);
            return RedirectToAction("Index", new { area = "Admin" });
        }*/
    }
}