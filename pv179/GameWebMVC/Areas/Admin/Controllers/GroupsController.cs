using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameWebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        #region Constants

        public const int PageSize = 10;

        #endregion Constants

        #region Facades

        public GroupFacade GroupFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }

        #endregion Facades

        public ActionResult Index()
        {
            return RedirectToAction("List", "Groups", new { area = "Admin" });
        }

        public async Task<ActionResult> List(int page = 1)
        {
            var filter = new GroupFilterDto { PageSize = PageSize, RequestedPageNumber = page };
            var result = await GroupFacade.GetGroupsByFilterAsync(filter);

            // Paging
            ViewBag.RequestedPageNumber = result.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)result.TotalItemsCount / (double)PageSize);
            // Paging END

            return View("List", result.Items);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await GroupFacade.GetGroupAsync(id);
            return View("Details", model);
        }

        public async Task<ActionResult> Edit(Guid id)
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
            await GroupFacade.RemoveGroup(id);
            return RedirectToAction("List", new { area = "Admin" });
        }
    }
}