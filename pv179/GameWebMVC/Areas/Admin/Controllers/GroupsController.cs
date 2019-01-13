using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameWebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        #region SessionKey constants

        public const int PageSize = 3;

        private readonly string pageNumberSessionKey = "pageNumber";

        private readonly string filterSessionKey = "filter";

        #endregion SessionKey constants

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
            Session[pageNumberSessionKey] = page;

            var filter = Session[filterSessionKey] as GroupFilterDto ?? new GroupFilterDto { PageSize = PageSize };
            filter.RequestedPageNumber = page;
            
            var result = await GroupFacade.GetGroupsByFilterAsync(filter);
            var collection = result.Items;

            // Paging
            ViewBag.RequestedPageNumber = result.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)result.TotalItemsCount / (double)PageSize);
            // Paging END

            /*
            if (collection == null)
                collection = new List<GroupDto>();
            */
            ViewBag.GroupMember = false;
            if (User.Identity.IsAuthenticated)
            {
                var user = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
                if (user.GroupId.HasValue)
                {
                    ViewBag.GroupMember = true;
                }
            }
            return View("List", collection);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await GroupFacade.GetGroupAsync(id);
            return View("Details", model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GroupDto group)
        {
            group.Picture = "/Img/default.jpg";
            var newGroupId = await GroupFacade.CreateGroup(Guid.Parse(User.Identity.Name), group, true);
            return RedirectToAction("Details", new { area = "Admin", id = newGroupId});
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
            var user = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));

            if ((user.IsGroupAdmin && user.GroupId != id) || User.IsInRole("Admin"))
            {
                await GroupFacade.RemoveGroup(id);
                return RedirectToAction("Index", new { area = "Admin" });
            }
            return RedirectToAction("NotAuthorized", "Error");
        }
    }
}