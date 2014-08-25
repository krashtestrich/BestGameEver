using System;
using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Shields;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests
{
    [TestClass]
    public class WhenPerformingActions
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfBattleOverAndActionPerformed()
        {
            var g = new Game();
            g.EndBattle(Alliance.TeamOne);
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, 0, 0);
            var tile = g.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Run"));
        }

        [TestMethod]
        public void ShouldPerformMoveAction()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var tile = g.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Run"));
            Assert.IsTrue(g.Player.ArenaLocation == tile);
            Assert.IsTrue(g.Player.CurrentAvailableActions == null);
        }

        [TestMethod]
        public void ShouldPerformSwingAttackAction()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.Arena.BuildArenaFloor(10);
            g.Player = new Player();
            g.Player.EquipEquipment(new Sword());
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            Assert.IsTrue(o.Health == 100);
            g.Arena.AddCharacterToArena(o,Alliance.TeamTwo, 0, 1);
            var tile = g.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health < 100);
            Assert.IsTrue(g.Player.CurrentAvailableActions == null);
        }

        [TestMethod]
        public void ShouldTakeLessDamageIfShieldEquipped()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.Arena.BuildArenaFloor(10);
            g.Player = new Player();
            g.Player.EquipEquipment(new ReliableTreeBranch());
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            o.EquipEquipment(new CrappyWoodenShield());
            o.SetArmor();
            g.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health > 70);
            Assert.IsTrue(o.Armor < 75);
        }
    }
}
