using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string text = "Error 404")
        {
            ViewBag.Message = text;
            return View();
        }

        public ActionResult NotAuthorized()
        {
            return RedirectToAction("Index", new {text = "Nedostatočné oprávnenia na túto akciu!"});
        }
    }
}