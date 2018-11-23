﻿using BL.DTO;
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
    public class GroupController : Controller
    {
        #region SessionKey constants

        public const int PageSize = 20;

        private readonly string pageNumberSessionKey = "pageNumber";

        private readonly string filterSessionKey = "filter";

        #endregion

        #region Facades
        public GroupFacade groupFacade { get; set; }
        #endregion




        // GET: Group
        public ActionResult Index()
        {
            var creator = Session["accountId"] as Guid?; // TODO - use when user is logged
            if (!creator.HasValue)
                return View("Login","Account");
            return View();
        }

        // GET: Group/Details/
        public async Task<ActionResult> Details(Guid id)
        {
            var model = await groupFacade.GetGroupAsync(id);
            return View("Details", model);
            //view members
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        public async Task<ActionResult> Create(GroupDto group)
        {
            try
            {
                var creator = Session["accountId"] as Guid?; // TODO - use when user is logged
                group.Picture = "/Img/default.jpg";
                if (!creator.HasValue)
                    return View("Login","Account");
                var newGroupId = await groupFacade.CreateGroup(creator.Value, group);
                return RedirectToAction("Details", new { id = newGroupId }); //redirect to detail
            }
            catch
            {
                return View(group);
            }
        }

        // GET: Group/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var group = await groupFacade.GetGroupAsync(id);
            return View(new GroupImageModel{ Group = group, File = null });
        }

        // POST: Group/Edit/5
        [HttpPost]
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
                await groupFacade.Edit(model.Group);
            }  
            catch (Exception ex)  
            {  
                ViewBag.Message = "ERROR: " + ex.Message.ToString();  
            }   
            return View(model);  

        }

        // GET: Group/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            // TO DO - check authorization
            await groupFacade.RemoveGroup(id);
            return RedirectToAction("List");
        }

        // GET: Group list
        public async Task<ActionResult> List(int page = 1)
        {
            Session[pageNumberSessionKey] = page;

            var filter = Session[filterSessionKey] as GroupFilterDto ?? new GroupFilterDto{PageSize = PageSize};
            filter.RequestedPageNumber = page;

            var result = await groupFacade.GetGroupsByFilterAsync(filter);

            return View("List", result.Items);
        }
    }
}
