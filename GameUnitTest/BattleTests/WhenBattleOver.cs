using System;
using System.Linq;
using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BattleTests
{
    [TestClass]
    public class WhenBattleOver
    {
        [TestMethod]
        public void ShouldGivePlayerCashWhenBotDefeated()
        {
            var g = new Game { Player = new Player() };
            var startCash = g.Player.Cash;
            g.Player.SetName("Player");
            var b = new Dumbass();
            b.SetName("Idiot");
            g.Tournament.AddCharacterToTournament(b);
            g.StartPlayerVsComputerGame();
            g.Arena.Characters.First(i => i is Dumbass).LoseHealth(100);
            g.PerformPlayerAction(
                g.Player.TargetTileAndSelectActions(g.Arena.SelectFloorTile(new ArenaFloorPosition(0, 3))).First()
            );
            g.ProcessBattleOver();
            Assert.IsTrue(g.Player.Cash > startCash);
        }

        [TestMethod]
        public void ShouldLevelPlayerUpWhenBotDefeated()
        {
            var g = new Game {Player = new Player()};
            g.Player.SetName("Player");
            var b = new Dumbass();
            b.SetName("Idiot");
            g.Tournament.AddCharacterToTournament(b);
            g.StartPlayerVsComputerGame();
            g.Arena.Characters.First(i => i is Dumbass).LoseHealth(100);
            g.PerformPlayerAction(
                g.Player.TargetTileAndSelectActions(g.Arena.SelectFloorTile(new ArenaFloorPosition(0,3))).First()
            );
            g.ProcessBattleOver();
            Assert.IsTrue(g.Player.GetLevel() == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfPostBattleProcessAttemptedBeforeBattleOver()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.ProcessBattleOver();
        }
    }
}
