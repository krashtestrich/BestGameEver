using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests
{
    [TestClass]
    public class WhenBotsPerformingActions
    {
        [TestMethod]
        public void ShouldPerformMoveActionWhenOutOfAttackRange()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var b = new Dumbass();
            g.Arena.AddCharacterToArena(b, Alliance.TeamTwo, 4, 4);
            g.PerformAITurn(Alliance.TeamTwo);
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 4 && endPosition.YCoord != 4);
        }

        [TestMethod]
        public void ShouldNotPerformMoveActionWhenPlayerTileSelected()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var b = new Dumbass();
            g.Arena.AddCharacterToArena(b, Alliance.TeamTwo, 0, 1);
            g.PerformAITurn(Alliance.TeamTwo);
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 0 || endPosition.YCoord != 0);
        }

        [TestMethod]
        public void ShouldPerformAttackActionWhenInAttackRange()
        {
            var g = new Game();
            g.StartBattle(BattleMode.PlayerVsComputer);
            g.Player = new Player();
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var b = new Dumbass();
            b.EquipEquipment(new Sword());
            g.Arena.AddCharacterToArena(b, Alliance.TeamTwo, 0, 1);
            g.PerformAITurn(Alliance.TeamTwo);
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord == 0 && endPosition.YCoord == 1);
            Assert.IsTrue(g.Player.Health < 100);
        }
    }
}
