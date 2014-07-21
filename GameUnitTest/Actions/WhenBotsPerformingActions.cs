using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.Actions
{
    [TestClass]
    public class WhenBotsPerformingActions
    {
        [TestMethod]
        public void ShouldPerformMoveAction()
        {
            var a = new Arena();
            a.BuildArenaFloor(10);
            var c = new Player();
            a.AddCharacterToArena(c, 0, 0);
            var b = new Dumbass(Alliance.Opponent);
            a.AddCharacterToArena(b, 5,5);
            a.PerformOpponentTurn();
            //Assert.IsTrue(c.ArenaLocation == tile);
        }
    }
}
