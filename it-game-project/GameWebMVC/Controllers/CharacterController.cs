using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace GameWebMVC.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        #region Constants

        public const int PageSize = 10;

        #endregion Constants

        #region Facades

        public CharacterFacade CharacterFacade { get; set; }
        public AccountFacade AccountFacade { get; set; }

        #endregion Facades

        public async Task<ActionResult> Index()
        {
            var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            if (character == null)
            {
                return RedirectToAction("Create");
            }
            return View(character);
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            var character = await CharacterFacade.GetCharacterById(id);
            if (character == null)
            {
                return RedirectToAction("Index", "Error", "Postava neexistuje");
            }
            return View(character);
        }

        #region Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CharacterDto characterDto)
        {
            try
            {
                var character = await CharacterFacade.CreateCharacter(Guid.Parse(User.Identity.Name), characterDto);

                var cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name, true);
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, false, ticket.UserData + ",HasCharacter");
                cookie.Value = FormsAuthentication.Encrypt(newTicket);
                cookie.Expires = newTicket.Expiration.AddMinutes(30);
                HttpContext.Response.Cookies.Set(cookie);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion Create

        #region Edit

        public async Task<ActionResult> Edit()
        {
            var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            return View(character);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CharacterDto characterDto)
        {
            try
            {
                var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
                character.Name = characterDto.Name;
                var res = await CharacterFacade.EditCharacter(character);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion Edit

        #region Remove

        public async Task<ActionResult> Remove()
        {
            var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            return View(character);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(CharacterDto characterDto)
        {
            var c = await CharacterFacade.RemoveCharacter(Guid.Parse(User.Identity.Name));

            var cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name, true);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, false, ticket.UserData.Replace(",HasCharacter", ""));
            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            cookie.Expires = newTicket.Expiration.AddMinutes(30);
            HttpContext.Response.Cookies.Set(cookie);

            return RedirectToAction("Create");
        }

        #endregion Remove

        [AllowAnonymous]
        public async Task<ActionResult> List(int page = 1)
        {
            var characters = await CharacterFacade.GetCharactersByFilterAsync(new CharacterFilterDto { PageSize = PageSize, RequestedPageNumber = page, SortCriteria = nameof(CharacterDto.Score) });

            // Paging
            ViewBag.RequestedPageNumber = characters.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)characters.TotalItemsCount / (double)PageSize);
            // Paging END

            return View(characters.Items);
        }
    }
}