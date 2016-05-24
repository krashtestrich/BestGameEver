using System;
using System.Linq;
using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Game;
using GameLogic.SkillTree.Paths.FighterPath;
using GameLogic.SkillTree.Paths.WizardPath;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.TournamentTests
{
    [TestClass]
    public class WhenCreatingTournament
    {
        [TestMethod]
        public void ShouldMakeSureParticipantNamesUnique()
        {
            var t = new Tournament();
            var c = new Dumbass();
            c.SetName("Idiot");
            t.AddCharacterToTournament(c);
            var d = new Dumbass();
            d.SetName("Idiot");
            t.AddCharacterToTournament(d);
            Assert.IsTrue(t.Participants.Count == 2 && t.Participants.Count(i => i.Character.Name == "Idiot") == 1);
        }

        [TestMethod]
        public void ShouldPopulateTournamentWithBots()
        {
            var t = new Tournament();
            t.Populate();
            Assert.IsTrue(t.Participants.Count == 128);
        }

        [TestMethod]
        public void ShouldAssignHealthAndSkillTreeWhenStartingTournament()
        {
            var t = new Tournament();
            t.Populate();
            t.Start();
            Assert.IsTrue(t.Participants.TrueForAll(i => i.Character.Health > 0));
            Assert.IsTrue(t.Participants.Exists(i => i.Character.SkillTree.Get().ToList().Exists(s => s is PathOfTheFighter && s.IsActive)));
            Assert.IsTrue(t.Participants.Exists(i => i.Character.SkillTree.Get().ToList().Exists(s => s is PathOfTheWizard && s.IsActive)));
        }

        [TestMethod]
        public void ShouldChooseBattleDetails()
        {
            var t = new Tournament();
            t.Populate();
            t.Start();
            var battleDetails = t.GetNextBattleDetails();
            Assert.IsNotNull(battleDetails);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfTournamentStartedWithoutEnoughValidParticipants()
        {
            var t = new Tournament();
            var c1 = new Dumbass();
            c1.SetName("1");
            t.Participants.Add(new Participant
            {
                Battles = 5,
                Character = c1,
                Status = ParticipantStatus.Active
            });
            var c2 = new Dumbass();
            c2.SetName("2");
            t.Participants.Add(new Participant
            {
                Battles = 1,
                Character = c2,
                Status = ParticipantStatus.KnockedOut
            });
            t.Start();
        }

        [TestMethod]
        public void ShouldStartComputerVsComputerTournament()
        { 
            var g = new Game(true, true);
            g.StartComputerVsComputerGame();
        }
    }
}
