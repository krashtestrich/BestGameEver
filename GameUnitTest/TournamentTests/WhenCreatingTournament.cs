using System.Linq;
using GameLogic.Characters.Bots;
using GameLogic.Game;
using GameLogic.SkillTree.Health;
using GameLogic.SkillTree.Mana;
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
            Assert.IsTrue(t.Participants.Count == 50);
        }

        [TestMethod]
        public void ShouldAssignHealthAndSkillTreeWhenStartingTournament()
        {
            var t = new Tournament();
            t.Populate();
            t.Start();
            Assert.IsTrue(t.Participants.TrueForAll(i => i.Character.Health > 0));
            Assert.IsTrue(t.Participants.Exists(i => i.Character.SkillTree.Get().ToList().Exists(s => s is HealthLevelOne && s.IsActive)));
            Assert.IsTrue(t.Participants.Exists(i => i.Character.SkillTree.Get().ToList().Exists(s => s is ManaLevelOne && s.IsActive)));
        }

        [TestMethod]
        public void ShouldChooseParticipant()
        {
            var t = new Tournament();
            t.Populate();
            t.Start();
            Assert.IsNotNull(t.ChooseCombatant());
        }

        [TestMethod]
        public void ShouldChooseParticipantParticipatedInLeastBattles()
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
                Status = ParticipantStatus.Active
            });
            t.Start();
            Assert.IsTrue(t.ChooseCombatant().Character == c2);
        }

        [TestMethod]
        public void ShouldOnlyChooseActiveParticipant()
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
            Assert.IsTrue(t.ChooseCombatant().Character == c1);
        }

        [TestMethod]
        public void ShouldReturnNullIfNoValidParticipantExists()
        {
            var t = new Tournament();
            var c1 = new Dumbass();
            c1.SetName("1");
            t.Participants.Add(new Participant
            {
                Battles = 5,
                Character = c1,
                Status = ParticipantStatus.KnockedOut
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
            Assert.IsNull(t.ChooseCombatant());
        }

        [TestMethod]
        public void ShouldStartComputerVsComputerTournament()
        {
            var g = new Game();
            g.StartComputerVsComputerGame();
        }

        [TestMethod]
        public void ShouldStartBattleWithTwoComputerCharacters()
        {
            var g = new Game();
            g.Tournament.Populate();
            g.Tournament.Start();
        }

        [TestMethod]
        public void ShouldKnockOutLoserWhenProcessingResult()
        {
            var g = new Game();
            g.Tournament.Populate();
            g.Tournament.Start();

        }
    }
}
