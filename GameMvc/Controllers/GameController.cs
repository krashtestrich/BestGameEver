using System;
using System.Linq;
using System.Web.Mvc;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
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
            var p = new Player();
            p.SetName(model.Name);
            Session["Player"] = p;
            var game = new Game();
            Session["Game"] = game;
            return View("~/Views/Home/Character.cshtml", p);

        }

        [HttpPost]
        public ActionResult CheckChooseOpponent()
        {
            var game = (Game) Session["Game"];
            if (game.GetGameStatus() == GameStatus.NotStarted)
            {
                return View("~/Views/Game/ChooseOpponent.cshtml", new UiAvailableOpponentsModel
                {
                    Opponents = game.GetPossibleOpponents()
                });
            }
            return null;
        }

        public ActionResult ChooseOpponent(string name)
        {
            var game = (Game) Session["Game"];
            if (game.GetGameStatus() != GameStatus.NotStarted)
            {
                throw new Exception("WTF YOU CANT CHOOSE AN OPPONENT YOU ALREADY HAVE ONE");
            }

            var opponent = game.GetPossibleOpponents().First(i => i.Name == name);
            if (opponent == null)
            {
                throw new Exception("THAT OPPONENT DOESNT EXIST WHAT ARE YOU DOING");
            }
            game.ChooseOpponent(opponent);
            game.StartBattle();
            Session["Game"] = game;

            return View("~/Views/Home/Arena.cshtml", game);
        }
    }
}
