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
    [Authorize(Roles = "HasCharacter")]
    public class GroupController : Controller
    {
        #region SessionKey constants

        public const int PageSize = 20;

        private readonly string pageNumberSessionKey = "pageNumber";

        private readonly string filterSessionKey = "filter";

        #endregion

        #region Facades
        public GroupFacade GroupFacade { get; set; }
        #endregion




        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await GroupFacade.GetGroupAsync(id);
            return View("Details", model);
            //view members
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GroupDto group)
        {
            group.Picture = "/Img/default.jpg";
            var newGroupId = await groupFacade.CreateGroup(Guid.Parse(User.Identity.Name), group);
            return RedirectToAction("Details", new { id = newGroupId });
        }

        // GET: Group/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var group = await GroupFacade.GetGroupAsync(id);
            return View(new GroupImageModel{ Group = group, File = null });
        }

        // POST: Group/Edit/5
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(GroupImageModel model)
        {
            try
            {
                var relativePath = "/Img/Default.jpg";
                if (model.File != null && model.File.ContentLength > 0)
                {
                    var fileType = Path.GetExtension(model.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img/"), model.Group.Id + fileType);
                    model.File.SaveAs(path);
                    relativePath = "/Img/" + model.Group.Id + fileType;
                }
                model.Group.Picture = relativePath;
                await GroupFacade.Edit(model.Group);
            }  
            catch (Exception ex)  
            {  
                ViewBag.Message = "ERROR: " + ex.Message.ToString();  
            }   
            return View(model);  

        }

        public async Task<ActionResult> Delete(Guid id)
        {
            // TO DO - check authorization
            await GroupFacade.RemoveGroup(id);
            return RedirectToAction("List");
        }

        // GET: Group list
        [AllowAnonymous]
        public async Task<ActionResult> List(int page = 1)
        {
            Session[pageNumberSessionKey] = page;

            var filter = Session[filterSessionKey] as GroupFilterDto ?? new GroupFilterDto{PageSize = PageSize};
            filter.RequestedPageNumber = page;

            var result = await GroupFacade.GetGroupsByFilterAsync(filter);

            return View("List", result.Items);
        }
    }
}
