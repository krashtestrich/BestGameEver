using GameLogic.Arena;
using GameLogic.Battle;
using GameLogic.Characters;
using GameLogic.Characters.Bots;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GameUnitTest
{
    [TestClass]
    public class WhenPerformingActions
    {
        [TestMethod]
        public void ShouldHaveAttackActionIfEnemyTargetSelected()
        {
            var c = new Character();
            c.SetCharacterLocation(0, 0);
            var b = new Dumbass(Alliance.Opponent);
            b.SetCharacterLocation(1, 1);
            c.EquipEquipment(new Sword());
            //Assert.IsTrue();
        }

        [TestMethod]
        public void ShouldPerformAttackActionWhenWithinRange()
        {
            var a = new Arena(new Battle());
            a.BuildArenaFloor(10);
            var c = new Character();
            a.AddCharacterToArena(c, 0, 0);
            var o = new Character();
            a.AddCharacterToArena(o, 1, 1);
            var e = new Sword();
            c.EquipEquipment(e);
            var tile = a.SelectFloorTile(o.ArenaLocation);
            var actions = c.SelectActionsFromTargetTile(tile);
            Assert.IsTrue(actions.Exists(i => i.Name == "Swing"));
        }

        [TestMethod]
        public void ShouldNotPerformAttackWhenOutOfRange()
        {
            var a = new Arena(new Battle());
            a.BuildArenaFloor(10);
            var c = new Character();
            a.AddCharacterToArena(c, 0, 0);
            var o = new Character();
            a.AddCharacterToArena(o, 5, 5);
            var e = new Sword();
            c.EquipEquipment(e);
            var tile = a.SelectFloorTile(o.ArenaLocation);
            var actions = c.SelectActionsFromTargetTile(tile);

            Assert.IsFalse(actions.Exists(i => i.Name == "Swing"));
        }
    }
}
