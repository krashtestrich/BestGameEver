using System.Linq;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BattleTests
{
    [TestClass]
    public class WhenSimulatingBattles
    {
        [TestMethod]
        public void ShouldSimulateAllBattles()
        {
            var g = new Game {Player = new Player(), Tournament = new Tournament()};
            g.Tournament.Populate();
            g.Tournament.Start();
            g.SimulateAllComputerBattles();
            Assert.IsTrue(g.Tournament.BattlesByRound[g.Tournament.Round].TrueForAll(b => 
                b.BattleMode == BattleMode.ComputerVsComputer 
                && b.BattleStatus == BattleStatus.BattleOver));
        }

        [TestMethod]
        public void ShouldSimulateBattle()
        {
            var g = new Game { Player = new Player(), Tournament = new Tournament() };
            g.Tournament.Populate();
            g.Tournament.Start();
            var battleDetails =
                g.Tournament.BattlesByRound[g.Tournament.Round].First(b => b.BattleMode == BattleMode.ComputerVsComputer);
            g.SimulateComputerVsComputerBattle(battleDetails.BattleGuid);
            Assert.IsTrue(battleDetails.BattleStatus == BattleStatus.BattleOver);
        }
    }
}
