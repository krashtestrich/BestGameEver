using System;
using System.Linq;
using System.Web.Mvc;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;

namespace GameMvc.Controllers
{
    public class TournamentController : Controller
    {
        [HttpPost]
        public ActionResult JoinTournament()
        {
            var g = (Game)Session["Game"];
            g.StartPlayerVsComputerTournament();
            g.Player = g.Tournament.Participants.First(i => i.Character is Player).Character as Player;
            g.CurrentBattleDetails.BattleStatus = BattleStatus.NotStarted;
            Session["Game"] = g;

            return View("~/Views/Home/Tournament.cshtml", g.Tournament);
        }

        [HttpPost]
        public ActionResult SimulateBattle(Guid battleGuid)
        {
            var g = (Game) Session["Game"];
            g.SimulateComputerVsComputerBattle(battleGuid);
            Session["Game"] = g;
            return View("~/Views/Home/Tournament.cshtml", g.Tournament);
        }

        [HttpPost]
        public ActionResult SimulateAll()
        {
            var g = (Game) Session["Game"];
            g.SimulateAllComputerBattles();
            Session["Game"] = g;
            return View("~/Views/Home/Tournament.cshtml", g.Tournament);
        }

        [HttpPost]
        public ActionResult WatchMatch(Guid battleGuid)
        {
            var g = (Game)Session["Game"];
            return View("~/Views/Home/Arena.cshtml", g.Tournament.BattlesByRound[g.Tournament.Round].ElementAt(0));
        }
    }
}