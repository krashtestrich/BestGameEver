using System;
using System.Linq;
using System.Web.Mvc;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Shields;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
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
            var g = (Game)Session["Game"];
            g.Player = new Player();
            g.Player.SetName(model.Name);
            return View("~/Views/Home/Character.cshtml", g.Player);

        }

        [HttpPost]
        public ActionResult CheckChooseOpponent()
        {
            var game = (Game) Session["Game"];
            if (game.GetBattleStatus() == BattleStatus.NotStarted)
            {
                return View("~/Views/Game/ChooseOpponent.cshtml", new UiAvailableOpponentsModel
                {
                    Opponents = game.GetPossibleOpponents()
                });
            }
            return null;
        }

        [HttpPost]
        public ActionResult CheckBattleStatus()
        {
            var game = (Game)Session["Game"];
            if (game.GetBattleStatus() != BattleStatus.BattleOver)
            {
                return null;
            }

            var br = game.BuildBattleReport();
            return View("~/Views/Game/BattleOver.cshtml", br);
        }

        [HttpPost]
        public ActionResult ProcessBattleOver()
        {
            var game = (Game) Session["Game"];
            game.ProcessBattleOver();
            Session["Game"] = game;
            return null;
        }

        public ActionResult ChooseOpponent(string name)
        {
            var game = (Game) Session["Game"];
            if (game.GetBattleStatus() != BattleStatus.NotStarted)
            {
                throw new Exception("WTF YOU CANT CHOOSE AN OPPONENT YOU ALREADY HAVE ONE");
            }

            var opponent = game.GetPossibleOpponents().First(i => i.Name == name);
            if (opponent == null)
            {
                throw new Exception("THAT OPPONENT DOESNT EXIST WHAT ARE YOU DOING");
            }
            opponent.EquipEquipment(new PieceofFoil());
            opponent.EquipEquipment(new Sword());
            game.ChooseOpponent(opponent);
            game.StartBattle(BattleMode.PlayerVsComputer);
            Session["Game"] = game;

            return View("~/Views/Home/Arena.cshtml", game);
        }

        public ActionResult PerformPlayerAction(string actionName)
        {
            var game = (Game) Session["Game"];
            var action = game.Player.CurrentAvailableActions.First(i => i.Name == actionName);
            game.PerformPlayerAction(action);
            game.PerformAITurn(Alliance.TeamTwo);
            Session["Game"] = game;
            return View("~/Views/Home/Arena.cshtml", game);
        }
    }
}
