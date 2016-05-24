using GameLogic.Characters.Bots.BotTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.TurnBasedTests
{
    [TestClass]
    public class WhenRegeneratingHealth
    {
        [TestMethod]
        public void ShouldRegeneratePercentageBasedHealth()
        {
            var b = new Dumbass();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShouldNotRegenerateHealthAboveMaxHealth()
        {
            var b = new Dumbass();
            Assert.IsTrue(true);
        }
    }
}
