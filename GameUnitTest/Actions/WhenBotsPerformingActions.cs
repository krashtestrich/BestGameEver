using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.Actions
{
    [TestClass]
    public class WhenBotsPerformingActions
    {
        [TestMethod]
        public void ShouldPerformMoveActionWhenOutOfAttackRange()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, 0, 0);
            var b = new Dumbass(Alliance.Opponent);
            a.AddCharacterToArena(b, 5,5);
            a.PerformOpponentTurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 5 && endPosition.YCoord != 5);
        }

        [TestMethod]
        public void ShouldPerformAttackActionWhenInAttackRange()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, 0, 0);
            var b = new Dumbass(Alliance.Opponent);
            b.EquipEquipment(new Sword());
            a.AddCharacterToArena(b, 0, 1);
            a.PerformOpponentTurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord == 0 && endPosition.YCoord == 1);
            Assert.IsTrue(c.Health < 100);
        }

    }
}
