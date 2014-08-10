using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.Actions
{
    [TestClass]
    public class WhenChoosingActions
    {
        [TestMethod]
        public void ShouldNotHaveAttackActionIfFriendlyTargetSelected()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Character(Alliance.Player, 1);
            a.AddCharacterToArena(c, 0, 0);
            var o = new Character(Alliance.Player, 1);
            a.AddCharacterToArena(o, 1, 1);
            var e = new Sword();
            c.EquipEquipment(e);
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
            var c = new Character(Alliance.Player, 1);
            a.AddCharacterToArena(c, 0, 0);
            var o = new Character(Alliance.Opponent, 1);
            a.AddCharacterToArena(o, 1, 1);
            var e = new Sword();
            c.EquipEquipment(e);
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
            var c = new Character(Alliance.Player, 1);
            a.AddCharacterToArena(c, 0, 0);
            var o = new Character(Alliance.Opponent, 1);
            a.AddCharacterToArena(o, 5, 5);
            var e = new Sword();
            c.EquipEquipment(e);
            var tile = a.SelectFloorTile(o.ArenaLocation.GetTileLocation());
            var actions = c.TargetTileAndSelectActions(tile);

            Assert.IsFalse(actions.Exists(i => i.Name == "Swing"));
        }
    }
}
