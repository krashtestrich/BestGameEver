using System.Linq;
using GameLogic.Characters.Bots;
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
    }
}
