using System.Web.Mvc;
using GameLogic.Arena;
using GameLogic.Characters.Player;
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
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }
            return View("Character", p);
        }

        public ActionResult Arena()
        {
            var p = (Player)Session["Player"];
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }

            var a = new Arena();
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(p);
            Session["Arena"] = a;

            return View("Arena", a);
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
