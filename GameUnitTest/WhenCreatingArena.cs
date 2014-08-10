using System;
using GameLogic.Characters;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Arena;

namespace GameUnitTest
{
    [TestClass]
    public class WhenCreatingArena
    {
        [TestMethod]
        public void ShouldAddCharacterToArena()
        {
            var a = new Arena();
            a.BuildArenaFloor(5);
            var c = new Character(Alliance.Neutral, 1);
            c.SetName("YoMomma");
            a.AddCharacterToArena(c);

            Assert.IsTrue(a.Characters.Exists(i => i.Name == "YoMomma"));
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldNotAddCharacterToArenaWhenFloorNotBuilt()
        {
            var a = new Arena();
            var c = new Character(Alliance.Neutral, 1);
            c.SetName("YoMomma");
            a.AddCharacterToArena(c);
        }

        [TestMethod]
        public void ShouldAddPlayerToArena()
        {
            var p = new Player();
            var a = new Arena();
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(p);
            Assert.IsTrue(a.Characters.Contains(p));
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldNotAddPlayerToArenaWhenFloorNotBuilt()
        {
            var p = new Player();
            var a = new Arena();
            a.AddCharacterToArena(p);
        }

        [TestMethod]
        public void ShouldAddPlayerToDefaultPosition()
        {
            var p = new Player();
            var a = new Arena();
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(p);
            Assert.IsTrue(p.ArenaLocation.GetTileLocation().XCoord == 0 && p.ArenaLocation.GetTileLocation().YCoord == 4);
        }

        [TestMethod]
        public void ShouldAddOpponentToDefaultPosition()
        {
            var b = new Dumbass(Alliance.Opponent, 1);
            var a = new Arena();
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(b);
            Assert.IsTrue(b.ArenaLocation.GetTileLocation().XCoord == 4 && b.ArenaLocation.GetTileLocation().YCoord == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotSetPlayerLocationIfArenaFloorNull()
        {
            var p = new Player();
            var a = new Arena();
            a.AddCharacterToArena(p);
        }

        [TestMethod]
        public void ShouldCreateArenaFloor()
        {
            var a = new Arena();
            a.BuildArenaFloor(5);

            Assert.IsTrue(a.ArenaFloor.Length == 25);
        }

        [TestMethod]
        public void ShouldGetDistanceBetweenFloorPositionsForShortDistance()
        {
            var p1 = new ArenaFloorPosition(0, 0);
            var p2 = new ArenaFloorPosition(1, 2);
            var d = ArenaHelper.GetDistanceBetweenFloorPositions(p1, p2);
            Assert.IsTrue(d == 2);
        }

        [TestMethod]
        public void ShouldGetDistanceBetweenFloorPositionsForLongDistance()
        {
            var p1 = new ArenaFloorPosition(0, 1);
            var p2 = new ArenaFloorPosition(10, 25);
            var d = ArenaHelper.GetDistanceBetweenFloorPositions(p1, p2);
            Assert.IsTrue(d == 26);
        }
    }
}
