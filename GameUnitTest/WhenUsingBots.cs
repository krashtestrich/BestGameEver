﻿using GameLogic.Characters.Bots;
using GameLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest
{
    [TestClass]
    public class WhenUsingBots
    {
        [TestMethod]
        public void ShouldRetrieveBotAlliance()
        {
            var bot = new Dumbass(Alliance.Neutral, 1);
            Assert.IsTrue(bot.GetAlliance() == Alliance.Neutral);
        }

        [TestMethod]
        public void ShouldChangeAlliance()
        {
            var bot = new Dumbass(Alliance.Neutral, 1);
            bot.ChangeAlliance(Alliance.Player);
            Assert.IsTrue(bot.GetAlliance() == Alliance.Player);
        }
    }
}
