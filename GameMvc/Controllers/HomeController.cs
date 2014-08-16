using System.Web.Mvc;
using GameLogic.Enums;
using GameLogic.Game;
using GameLogic.Shop;

namespace GameMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var g = (Game) Session["Game"];
            if (g != null)
            {
                return View();
            }
            g = new Game();
            Session["Game"] = g;
            return View();
        }

        public ActionResult Character()
        {
            var g = (Game)Session["Game"];
            if (g.Player != null)
            {
                if (g.GetBattleStatus() != BattleStatus.InBattle)
                {
                    g.Player.LeaveArena();
                    Session["Game"] = g;
                    return View("Character", g.Player);
                }
                ModelState.AddModelError(string.Empty, "You cannot leave the battle!");
                return View("Arena", g);
            }
            ModelState.AddModelError(string.Empty, "You must create a character first.");
            return View("Index");
        }

        public ActionResult Arena()
        {
            var g = (Game)Session["Game"];
            if (g.Player == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }

            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            Session["Game"] = g;

            return View("Arena", g);
        }

        public ActionResult Shop()
        {
            var g = (Game)Session["Game"];
            if (g.Player == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }
            if (g.GetBattleStatus() != BattleStatus.InBattle)
            {
                g.Player.LeaveArena();
                var s = new Shop();
                s.AddPlayerToShop(g.Player);
                return View("Shop", s);
            }
            ModelState.AddModelError(string.Empty, "You cannot leave the battle!");
            return View("Arena", g);
        }
    }
}
