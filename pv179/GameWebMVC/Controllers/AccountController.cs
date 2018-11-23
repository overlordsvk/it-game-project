﻿using BL.DTO;
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
    public class AccountController : Controller
    {
        public AccountFacade accountFacade { get; set; }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public async Task<ActionResult> Register(AccountCreateDto createDto)
        {
            try
            {
                var accountId = await accountFacade.RegisterAccount(createDto);
                Session["accountId"] = accountId;
                return RedirectToAction("Create", "Character");
            }
            catch (Exception)
            {
                return View();
            }

        }

        // GET: Account/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public async Task<ActionResult> Login(AccountLogin login)
        {
            try
            {
                var account = await accountFacade.Login(login.usernameOrEmail, login.password);
                if (account == null)
                {
                    return View();
                }
                Session["accountId"] = account.Id;
                if (account.Character == null)
                {
                    return RedirectToAction("Create", "Character");
                }
                return RedirectToAction("Index", "Character");
            }
            catch (Exception)
            {
                return View();
            }

        }


        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}