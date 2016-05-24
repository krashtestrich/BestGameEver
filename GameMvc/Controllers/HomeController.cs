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
                if (g.CurrentBattleDetails.BattleStatus != BattleStatus.InBattle)
                {
                    g.Player.LeaveArena();
                    Session["Game"] = g;
                    return View("Character", g.Player);
                }
                ModelState.AddModelError(string.Empty, "You cannot leave the battle!");
                return View("Arena", g.CurrentBattleDetails);
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

            if (g.CurrentBattleDetails.BattleMode != BattleMode.PlayerVsComputer)
            {
                ModelState.AddModelError(string.Empty, "Currently cannot visit Arena when not Player vs Computer mode");
                return View("Character", g.Player);
            }
            g.CurrentBattleDetails.BattleStatus = BattleStatus.InBattle;
            Session["Game"] = g;

            return View("Arena", g.CurrentBattleDetails);
        }

        public ActionResult Shop()
        {
            var g = (Game)Session["Game"];
            if (g.Player == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }

            if (g.CurrentBattleDetails.BattleStatus != BattleStatus.InBattle)
            {
                g.Player.LeaveArena();
                var s = new Shop();
                s.AddPlayerToShop(g.Player);
                return View("Shop", s);
            }
            ModelState.AddModelError(string.Empty, "You cannot leave the battle!");
            return View("Arena", g.CurrentBattleDetails);
        }

        public ActionResult Tournament()
        {
            var g = (Game)Session["Game"];
            if (g.Player == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }
            if (g.Player.SkillPoints > 0)
            {
                ModelState.AddModelError(string.Empty, "You must assign your skillpoints first.");
                return View("Character", g.Player);
            }
            if (g.CurrentBattleDetails.BattleStatus != BattleStatus.InBattle)
            {
                g.Player.LeaveArena();
                return View("Tournament", g.Tournament);
            }
            ModelState.AddModelError(string.Empty, "You cannot leave the battle!");
            return View("Arena", g.CurrentBattleDetails);
        }
    }
}
