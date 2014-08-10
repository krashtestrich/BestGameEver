using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.Actions
{
    [TestClass]
    public class WhenBotsPerformingActions
    {
        [TestMethod]
        public void ShouldPerformMoveActionWhenOutOfAttackRange()
        {
            var g = new Game();
            g.StartBattle();
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, 0, 0);
            var b = new Dumbass(Alliance.Opponent, 1);
            g.Arena.AddCharacterToArena(b, 4, 4);
            g.PerformOpponentTurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 4 && endPosition.YCoord != 4);
        }

        [TestMethod]
        public void ShouldNotPerformMoveActionWhenPlayerTileSelected()
        {
            var g = new Game();
            g.StartBattle();
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, 0, 0);
            var b = new Dumbass(Alliance.Opponent, 1);
            g.Arena.AddCharacterToArena(b, 0, 1);
            g.PerformOpponentTurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 0 || endPosition.YCoord != 0);
        }

        [TestMethod]
        public void ShouldPerformAttackActionWhenInAttackRange()
        {
            var g = new Game();
            g.StartBattle();
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, 0, 0);
            var b = new Dumbass(Alliance.Opponent, 1);
            b.EquipEquipment(new Sword());
            g.Arena.AddCharacterToArena(b, 0, 1);
            g.PerformOpponentTurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord == 0 && endPosition.YCoord == 1);
            Assert.IsTrue(g.Player.Health < 100);
        }
    }
}
