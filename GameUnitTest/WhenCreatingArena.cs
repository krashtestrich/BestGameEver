using System;
using GameLogic.Battle;
using GameLogic.Characters;
using GameLogic.Characters.Player;
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
            var a = new Arena(new Battle());
            a.BuildArenaFloor(5);
            var c = new Character();
            c.SetName("YoMomma");
            a.AddCharacterToArena(c);

            Assert.IsTrue(a.Characters.Exists(i => i.Name == "YoMomma"));
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldNotAddCharacterToArenaWhenFloorNotBuilt()
        {
            var a = new Arena(new Battle());
            var c = new Character();
            c.SetName("YoMomma");
            a.AddCharacterToArena(c);
        }

        [TestMethod]
        public void ShouldAddPlayerToArena()
        {
            var p = new Player();
            var a = new Arena(new Battle());
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(p);
            Assert.IsTrue(a.Player == p);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldNotAddPlayerToArenaWhenFloorNotBuilt()
        {
            var p = new Player();
            var a = new Arena(new Battle());
            a.AddCharacterToArena(p);
        }

        [TestMethod]
        public void ShouldAddPlayerToDefaultBottomCentreOfArenaForOddWidth()
        {
            var p = new Player();
            var a = new Arena(new Battle());
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(p);
            Assert.IsTrue(p.ArenaLocation.XCoord == a.ArenaFloor.GetLength(0) - 1 && p.ArenaLocation.YCoord == 2);
        }

        [TestMethod]
        public void ShouldAddPlayerToDefaultBottomLeftCentreOfArenaForEvenWidth()
        {
            var p = new Player();
            var a = new Arena(new Battle());
            a.BuildArenaFloor(8);
            a.AddCharacterToArena(p);
            Assert.IsTrue(p.ArenaLocation.XCoord == a.ArenaFloor.GetLength(0) - 1 && p.ArenaLocation.YCoord == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotSetPlayerLocationIfArenaFloorNull()
        {
            var p = new Player();
            var a = new Arena(new Battle());
            a.AddCharacterToArena(p);
        }

        [TestMethod]
        public void ShouldCreateArenaFloor()
        {
            var a = new Arena(new Battle());
            a.BuildArenaFloor(5);

            Assert.IsTrue(a.ArenaFloor.Length == 25);
        }

        [TestMethod]
        public void ShouldGetDistanceBetweenFloorPositionsForShortDistance()
        {
            var p1 = new ArenaFloorPosition(0, 0);
            var p2 = new ArenaFloorPosition(1, 2);
            var d = ArenaHelper.GetDistanceBetweenFloorPositions(p1, p2);
            Assert.IsTrue(d == 1);
        }

        [TestMethod]
        public void ShouldGetDistanceBetweenFloorPositionsForLongDistance()
        {
            var p1 = new ArenaFloorPosition(0, 1);
            var p2 = new ArenaFloorPosition(10, 25);
            var d = ArenaHelper.GetDistanceBetweenFloorPositions(p1, p2);
            Assert.IsTrue(d == 10);
        }
    }
}
