using System;
using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests
{
    [TestClass]
    public class WhenPerformingActions
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfBattleOverAndActionPerformed()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleStatus = BattleStatus.InBattle,
                    BattleMode = BattleMode.PlayerVsComputer
                },
                Player = new Player()
            };
            g.Player.SetName("Player");
            g.Player.ChangeAlliance(Alliance.TeamOne);
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Character = g.Player,
                Status = ParticipantStatus.InBattle
            });
            var b = new Dumbass();
            b.ChangeAlliance(Alliance.TeamTwo);
            b.SetName("Dumbass");
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Character = b,
                Status = ParticipantStatus.InBattle
            });
            g.EndBattle(Alliance.TeamOne);

            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, 0, 0);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Run"));
        }

        [TestMethod]
        public void ShouldPerformMoveAction()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleStatus = BattleStatus.InBattle,
                    BattleMode = BattleMode.PlayerVsComputer
                },
                Player = new Player()
            };
            g.Player.SetName("Player");
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Character = g.Player,
                Status = ParticipantStatus.InBattle
            });
            var b = new Dumbass();
            b.SetName("Dumbass");
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Character = b,
                Status = ParticipantStatus.InBattle
            });
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i.Name == "Run"));
            Assert.IsTrue(g.Player.ArenaLocation == tile);
            Assert.IsTrue(g.Player.CurrentAvailableActions == null);
        }
    }
}
