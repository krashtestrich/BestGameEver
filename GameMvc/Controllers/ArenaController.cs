using System.Web.Mvc;
using GameLogic.Arena;
using GameLogic.Game;
using GameMvc.Models;

namespace GameMvc.Controllers
{
    public class ArenaController : Controller
    {
        [HttpPost]
        public ActionResult TargetTile(UiTileSelectModel model)
        {
            var g = (Game) Session["Game"];
            var actions = g.Player.TargetTileAndSelectActions(g.Arena.SelectFloorTile(new ArenaFloorPosition(model.XCoord, model.YCoord)));
            Session["Game"] = g;
            return View("~/Views/Game/Arena/Actions.cshtml", new UiActionModel
            {
                Actions = actions
            });
        }
    }
}