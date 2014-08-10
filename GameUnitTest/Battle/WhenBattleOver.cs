using System;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.Battle
{
    [TestClass]
    public class WhenBattleOver
    {
        [TestMethod]
        public void ShouldGivePlayerCashWhenBotDefeated()
        {
            var g = new Game();
            g.StartBattle();
            g.Player = new Player();
            var startCash = g.Player.Cash;
            g.Player.EquipEquipment(new MegaSwordOfDeath());
            g.Arena.AddCharacterToArena(g.Player, 0, 0);
            var b = new Dumbass(Alliance.Opponent, 1);
            g.Arena.AddCharacterToArena(b, 0, 1);
            g.PerformPlayerAction(
                g.Player.TargetTileAndSelectActions(b.ArenaLocation).First(i => i is Attack)
            );
            g.ProcessBattleOver();
            Assert.IsTrue(g.GetBattleStatus() == BattleStatus.NotStarted);
            Assert.IsTrue(g.Player.Cash > startCash);
        }

        [TestMethod]
        public void ShouldLevelPlayerUpWhenBotDefeated()
        {
            var g = new Game();
            g.StartBattle();
            g.Player = new Player();
            g.Player.EquipEquipment(new MegaSwordOfDeath());
            g.Arena.AddCharacterToArena(g.Player, 0, 0);
            var b = new Dumbass(Alliance.Opponent, 1);
            g.Arena.AddCharacterToArena(b, 0, 1);
            g.PerformPlayerAction(
                g.Player.TargetTileAndSelectActions(b.ArenaLocation).First(i => i is Attack)
            );
            g.ProcessBattleOver();
            Assert.IsTrue(g.Player.GetLevel() == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfPostBattleProcessAttemptedBeforeBattleOver()
        {
            var g = new Game();
            g.StartBattle();
            g.ProcessBattleOver();
        }
    }
}
