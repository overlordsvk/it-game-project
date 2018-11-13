using BL.DTO;
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
        public CharacterFacade _characterFacade { get; set; }
        
        // GET: Character
        public ActionResult Index()
        {
            var model = GetCharacter(Guid.Parse("3454b2db-4bb1-4ffa-a3e9-be8c9617252d")).Result;
            return View("Index", model);
        }

        public async Task<CharacterDto> GetCharacter(Guid Id)
        {
            var x = await _characterFacade.GetCharacterById(Guid.Parse("3454b2db-4bb1-4ffa-a3e9-be8c9617252d"));
            return x;
        }

    }
}