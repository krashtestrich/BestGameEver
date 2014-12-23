using GameLogic.Characters.Bots;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using GameLogic.Game;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests
{
    [TestClass]
    public class WhenBotsPerformingActions
    {
        [TestMethod]
        public void ShouldPerformMoveActionWhenOutOfAttackRange()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamTwo
                },
                Player = new Player()
            };
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var b = new Dumbass();
            b.LevelUp();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(b, Alliance.TeamTwo, 4, 4);
            g.PerformAITurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 4 && endPosition.YCoord != 4);
        }

        [TestMethod]
        public void ShouldNotPerformMoveActionWhenPlayerTileSelected()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamTwo
                },
                Player = new Player()
            };
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var b = new Dumbass();
            b.LevelUp();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(b, Alliance.TeamTwo, 0, 1);
            g.PerformAITurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord != 0 || endPosition.YCoord != 0);
        }

        [TestMethod]
        public void ShouldPerformAttackActionWhenInAttackRange()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamTwo
                },
                Player = new Player()
            };
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var b = new Dumbass();
            b.LevelUp();
            EquipmentHelper.EquipEquipment(b, new Sword());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(b, Alliance.TeamTwo, 0, 1);
            g.PerformAITurn();
            var endPosition = b.ArenaLocation.GetTileLocation();
            Assert.IsTrue(endPosition.XCoord == 0 && endPosition.YCoord == 1);
            Assert.IsTrue(g.Player.Health < 100);
        }
    }
}
