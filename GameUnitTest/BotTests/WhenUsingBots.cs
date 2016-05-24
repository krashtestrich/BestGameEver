using GameLogic.Characters.Bots;
using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BotTests
{
    [TestClass]
    public class WhenUsingBots
    {
        [TestMethod]
        public void ShouldChangeAlliance()
        {
            var bot = new Dumbass();
            bot.ChangeAlliance(Alliance.TeamOne);
            Assert.IsTrue(bot.GetAlliance() == Alliance.TeamOne);
        }
    }
}
