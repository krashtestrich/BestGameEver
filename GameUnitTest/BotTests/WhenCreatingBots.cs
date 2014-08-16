using GameLogic.Characters.Bots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BotTests
{
    [TestClass]
    public class WhenCreatingBots
    {
        [TestMethod]
        public void ShouldBeAssignedRandomName()
        {
            var b = new Dumbass();
            Assert.IsTrue(!string.IsNullOrEmpty(b.Name));
        }
    }
}
