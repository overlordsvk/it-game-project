using BL.DTO.Filters;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        #region Constants

        public const int PageSize = 10;

        #endregion Constants

        #region Facades

        public AccountFacade AccountFacade { get; set; }

        #endregion Facades

        // GET: Admin/Accounts
        public ActionResult Index()
        {
            return RedirectToAction("List", "Accounts", new { area = "Admin" });
        }

        public async Task<ActionResult> List(int page = 1)
        {
            var filter = new AccountFilterDto { PageSize = PageSize, RequestedPageNumber = page};
            var result = await AccountFacade.ListAccountsAsync(filter);

            // Paging
            ViewBag.RequestedPageNumber = result.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)result.TotalItemsCount / (double)PageSize);
            // Paging END

            return View("List", result.Items);
        }
    }
}