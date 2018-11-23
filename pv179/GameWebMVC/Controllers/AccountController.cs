using BL.DTO;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GameWebMVC.Controllers
{
    public class AccountController : Controller
    {
        public AccountFacade AccountFacade { get; set; }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(AccountCreateDto accountCreateDto)
        {
            try
            {
                var id = await AccountFacade.RegisterAccount(accountCreateDto);
                //FormsAuthentication.SetAuthCookie(id.ToString(), false);

                var authTicket = new FormsAuthenticationTicket(1, id.ToString(), DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Create", "Character");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
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
        public async Task<ActionResult> Login(AccountLoginModel login, string returnUrl)
        {

            (bool success, Guid id, string roles) = await AccountFacade.Login(login.usernameOrEmail, login.password);
            if (success)
            {
                //FormsAuthentication.SetAuthCookie(id.ToString(), false);

                var authTicket = new FormsAuthenticationTicket(1, id.ToString(), DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                var decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    decodedUrl = Server.UrlDecode(returnUrl);
                }

                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            //var customer = await AccountFacade.GetAccountAccordingToUsernameAsync(User.Identity.Name);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}
