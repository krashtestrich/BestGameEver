using GameLogic.Arena;
using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Shields;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests.Physical
{
    [TestClass]
    public class WhenPerformingPhysicalAttacks
    {
        [TestMethod]
        public void ShouldPerformSwingAttackAction()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.CurrentBattleDetails.Arena.BuildArenaFloor(10);
            EquipmentHelper.EquipEquipment(g.Player, new Sword());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            Assert.IsTrue(o.Health == 100);
            g.CurrentBattleDetails.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health < 100);
            Assert.IsTrue(g.Player.CurrentAvailableActions == null);
        }

        [TestMethod]
        public void ShouldTakeLessDamageIfShieldEquipped()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.CurrentBattleDetails.Arena.BuildArenaFloor(10);
            EquipmentHelper.EquipEquipment(g.Player, new ReliableTreeBranch());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            EquipmentHelper.EquipEquipment(o, new CrappyWoodenShield());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health > 70);
        }

        [TestMethod]
        public void ShouldAddPhysicalDamageToAttack()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            EquipmentHelper.EquipEquipment(g.Player, new ReliableTreeBranch());
            g.Player.AddPhysicalDamage(10);
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health == 60);
        }

        [TestMethod]
        public void ShouldApplyPhysicalDamagePercentageToAttack()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.CurrentBattleDetails.Arena.BuildArenaFloor(10);
            EquipmentHelper.EquipEquipment(g.Player, new ReliableTreeBranch());
            g.Player.AddPhysicalDamageBonusPercent(50);
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health == 55);
        }
    }
}
