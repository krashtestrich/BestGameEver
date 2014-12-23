using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests
{
    [TestClass]
    public class WhenChoosingActions
    {
        [TestMethod]
        public void ShouldNotHaveAttackActionIfFriendlyTargetSelected()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            a.AddCharacterToArena(o, Alliance.TeamOne, 1, 1);
            var e = new Sword();
            EquipmentHelper.EquipEquipment(c, e);
            var tile = a.SelectFloorTile(o.ArenaLocation.GetTileLocation());
            var actions = c.TargetTileAndSelectActions(tile);
            Assert.IsFalse(actions.Exists(i => i.Name == "Swing"));
            Assert.IsFalse(c.CurrentAvailableActions.Exists(i => i.Name == "Swing"));
        }

        [TestMethod]
        public void ShouldHaveAttackActionWhenWithinRange()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            a.AddCharacterToArena(o, Alliance.TeamTwo, 1, 1);
            var e = new Sword();
            EquipmentHelper.EquipEquipment(c, e);
            var tile = a.SelectFloorTile(o.ArenaLocation.GetTileLocation());
            var actions = c.TargetTileAndSelectActions(tile);
            Assert.IsTrue(actions.Exists(i => i.Name == "Swing"));
            Assert.IsTrue(c.CurrentAvailableActions.Exists(i => i.Name == "Swing"));
        }

        [TestMethod]
        public void ShouldNotHaveAttackActionWhenOutOfRange()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            a.AddCharacterToArena(o, Alliance.TeamTwo, 5, 5);
            var e = new Sword();
            EquipmentHelper.EquipEquipment(c, e);
            var tile = a.SelectFloorTile(o.ArenaLocation.GetTileLocation());
            var actions = c.TargetTileAndSelectActions(tile);

            Assert.IsFalse(actions.Exists(i => i.Name == "Swing"));
        }
    }
}
