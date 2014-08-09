using System.Web.Mvc;
using GameLogic.Arena;
using GameLogic.Characters.Player;
using GameMvc.Models;

namespace GameMvc.Controllers
{
    public class ArenaController : Controller
    {
        [HttpPost]
        public ActionResult TargetTile(UiTileSelectModel model)
        {
            var p = (Player)Session["Player"];
            var a = (Arena)Session["Arena"];
            var actions = p.TargetTileAndSelectActions(a.SelectFloorTile(new ArenaFloorPosition(model.XCoord, model.YCoord)));
            Session["Player"] = p;
            return View("~/Views/Game/Arena/Actions.cshtml", new UiActionModel
            {
                Actions = actions
            });
        }
    }
}