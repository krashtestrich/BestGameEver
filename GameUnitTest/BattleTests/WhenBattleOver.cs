using System;
using System.Linq;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BattleTests
{
    [TestClass]
    public class WhenBattleOver
    {
        [TestMethod]
        public void ShouldGivePlayerCashWhenBotDefeated()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            var startCash = g.Player.Cash;
            g.Player.SetName("Player");
            var b = new Dumbass();
            b.SetName("Idiot");
            g.Tournament.AddCharacterToTournament(b);
            g.StartPlayerVsComputerTournament();
            g.CurrentBattleDetails.Arena.Characters.First(i => i is Dumbass).LoseHealth(200);
            g.EndBattle(g.Player.GetAlliance());
            g.ProcessBattleOver();
            Assert.IsTrue(g.Player.Cash > startCash);
        }

        [TestMethod]
        public void ShouldLevelPlayerUpWhenBotDefeated()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            }; 
            g.Player.SetName("Player");
            var b = new Dumbass();
            b.SetName("Idiot");
            g.Tournament.AddCharacterToTournament(b);
            g.StartPlayerVsComputerTournament();
            g.CurrentBattleDetails.Arena.Characters.First(i => i is Dumbass).LoseHealth(200);
            g.EndBattle(g.Player.GetAlliance());
            g.ProcessBattleOver();
            Assert.IsTrue(g.Player.GetLevel() == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfPostBattleProcessAttemptedBeforeBattleOver()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleStatus = BattleStatus.InBattle
                }
            };
            g.ProcessBattleOver();
        }

        [TestMethod]
        public void ShouldResetCharacterAfterBattle()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.Player.SetName("Player");
            var b = new Dumbass();
            b.SetName("Idiot");
            g.Tournament.AddCharacterToTournament(b);
            g.StartPlayerVsComputerTournament();
            g.CurrentBattleDetails.Arena.Characters.First(i => i is Player).LoseHealth(50);
            Assert.IsTrue(g.Player.Health == 50);
            g.EndBattle(g.Player.GetAlliance());
            g.ProcessBattleOver();
            Assert.IsTrue(g.Player.Health == 100);
        }
    }
}
