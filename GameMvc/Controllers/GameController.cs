using System.Web.Mvc;
using GameLogic.Characters.Player;
using GameMvc.Models;

namespace GameMvc.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        [HttpPost]
        public ActionResult CreateCharacter(UiCharacterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", model);
            }
            var p = new Player();
            p.SetName(model.Name);
            Session["Player"] = p;
            return View("~/Views/Home/Character.cshtml", p);
        }
    }
}
