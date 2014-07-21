using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.Actions
{
    [TestClass]
    public class WhenPerformingActions
    {
        [TestMethod, Ignore]
        public void ShouldPerformRunAction()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, 0, 0);
            var tile = a.SelectFloorTile(new ArenaFloorPosition(1,2));
            var actions = c.TargetTileAndSelectActions(tile);
            a.PerformPlayerAction(actions.Find(i => i.Name == "Run"));
            Assert.IsTrue(c.ArenaLocation == tile);
        }

        [TestMethod]
        public void ShouldPerformSwingAttackAction()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            c.EquipEquipment(new Sword());
            a.AddCharacterToArena(c, 0, 0);
            var o = new Dumbass(Alliance.Opponent);
            Assert.IsTrue(o.Health == 100);
            a.AddCharacterToArena(o,0,1);
            var tile = a.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = c.TargetTileAndSelectActions(tile);
            a.PerformPlayerAction(actions.Find(i => i.Name == "Swing"));
            Assert.IsTrue(o.Health < 100);
        }
    }
}
