using System.Linq;
using GameLogic.Characters.Bots;
using GameLogic.Enums;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.GameTests
{
    [TestClass]
    public class WhenPlayingGame
    {
        [TestMethod]
        public void ShouldGetPossibleOpponents()
        {
            var g = new Game();
            Assert.IsTrue(g.GetPossibleOpponents().Exists(i => i.GetType() == typeof(Dumbass)));
        }

        [TestMethod]
        public void ShouldHaveDefaultStatusOfNotStarted()
        {
            var g = new Game();
            Assert.IsTrue(g.GetBattleStatus() == BattleStatus.NotStarted);
        }

        [TestMethod]
        public void ShouldAddOpponentToArenaWhenChosen()
        {
            var g = new Game();
            var opponent = g.GetPossibleOpponents().First();
            g.ChooseOpponent(opponent);
            Assert.IsTrue(g.Arena.Characters.Exists(i => i == opponent));
            Assert.IsTrue(g.ChosenOpponent == opponent);
        }
    }
}
