using System.Web.Mvc;
using GameLogic.Characters.Player;
using GameLogic.Game;
using GameLogic.Shop;

namespace GameMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Character()
        {
            var p = (Player)Session["Player"];
            if (p != null)
            {
                return View("Character", p);
            }
            ModelState.AddModelError(string.Empty, "You must create a character first.");
            return View("Index");
        }

        public ActionResult Arena()
        {
            var p = (Player)Session["Player"];
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }

            var game = (Game) Session["Game"];
            game.Arena.AddCharacterToArena(p);
            Session["Game"] = game;

            return View("Arena", game);
        }

        public ActionResult Shop()
        {
            var p = (Player)Session["Player"];
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }
            var s = new Shop();
            s.AddPlayerToShop(p);

            return View("Shop", s);
        }
    }
}
