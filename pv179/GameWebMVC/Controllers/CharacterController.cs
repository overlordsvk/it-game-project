using BL.DTO;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public async Task<ActionResult> Index()
        {
            var id = Session["accountId"] as Guid?;

            var character = await characterFacade.GetCharacterById(id.Value);
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
                var id = Session["accountId"] as Guid?;
                characterDto.Health = characterDto.Endurance * 10;
                var character = await characterFacade.CreateCharacter(id.Value, characterDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit()
        {
            var id = Session["accountId"] as Guid?;

            var character = await characterFacade.GetCharacterById(id.Value);
            return View(character);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CharacterDto characterDto)
        {
            try
            {
                var id = Session["accountId"] as Guid?;
                var character = await characterFacade.GetCharacterById(id.Value);
                character.Name = characterDto.Name;
                var res = await characterFacade.EditCharacter(id.Value, character);
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }

        }
        #endregion

        public async Task<ActionResult> Remove()
        {
            var id = Session["accountId"] as Guid?;

            var character = await characterFacade.GetCharacterById(id.Value);
            return View(character);
        }
        [HttpPost]
        public async Task<ActionResult> Remove(CharacterDto characterDto)
        {
            var id = Session["accountId"] as Guid?;
            var c = await characterFacade.RemoveCharacter(id.Value);
            return RedirectToAction("Create");
        }
    }

}