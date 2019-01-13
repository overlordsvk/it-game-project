using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            return View();
        }
    }
}