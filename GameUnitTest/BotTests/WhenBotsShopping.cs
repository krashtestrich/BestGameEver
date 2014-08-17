using GameLogic.Characters.Bots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BotTests
{
    [TestClass]
    public class WhenBotsShopping
    {
        [TestMethod]
        public void ShouldPurchaeItems()
        {
            var b = new Dumbass();
            b.LevelUp();
            b.SetCash(100);
            b.BuyItems();
        }
    }
}
