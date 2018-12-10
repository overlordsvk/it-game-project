using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        #region SessionKey constants

        public const int PageSize = 20;

        private readonly string pageNumberSessionKey = "pageNumber";

        private readonly string filterSessionKey = "filter";

        #endregion

        #region Facades
        public GroupFacade GroupFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [Route("Admin/Groups")]
        public async Task<ActionResult> Groups(int page = 1)
        {
            Session[pageNumberSessionKey] = page;

            var filter = Session[filterSessionKey] as GroupFilterDto ?? new GroupFilterDto{PageSize = PageSize};
            filter.RequestedPageNumber = page;

            var result = await GroupFacade.GetGroupsByFilterAsync(filter);

            return View("GroupList", result.Items);
        }

        [Route("Admin/Groups")]
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await GroupFacade.GetGroupAsync(id);
            return View("GroupDetails", model);
            //view members
        }

        [Route("Admin/Groups")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("Admin/Groups")]
        public async Task<ActionResult> Create(GroupDto group)
        {
            group.Picture = "/Img/default.jpg";
            var newGroupId = await GroupFacade.CreateGroup(Guid.Parse(User.Identity.Name), group);
            return RedirectToAction("GroupDetails", new { id = newGroupId });
        }

        [Authorize(Roles = "Admin"), Route("Admin/Groups")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var group = await GroupFacade.GetGroupAsync(id);
            return View("GroupEdit", new GroupImageModel{ Group = group, File = null });
        }

        [HttpPost, Authorize(Roles = "Admin"), Route("Admin/Groups")]
        public async Task<ActionResult> Edit(GroupImageModel model)
        {
            try
            {
                var relativePath = model.Group.Picture;
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
            return View("GroupEdit", model);
        }

        [Route("Admin/Groups")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            
            if ((user.IsGroupAdmin && user.GroupId != id) || User.IsInRole("Admin"))
            {
                await GroupFacade.RemoveGroup(id);
                return View("GroupList");
                
            }
            return RedirectToAction("NotAuthorized", "Error");
        }
    }
}
