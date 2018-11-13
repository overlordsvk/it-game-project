using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    public class CharacterController : Controller
    {
        public CharacterFacade characterFacade { get; set; }

        // GET: Character
        public async Task<ActionResult> Index(Guid id)
        {
            var character = await characterFacade.GetCharacterById(id);
            return View(character);
        }
    }
}