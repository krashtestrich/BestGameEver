using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.HealthRegeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingHealthRegenerationModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageHealth()
        {
            const int playerBonusHealthRegeneration = 1;
            var p = new Player();
            p.AddModifier(new HealthRegenerationPercentage(100));
            Assert.IsTrue(p.BonusHealthRegeneration == playerBonusHealthRegeneration);
        }

        [TestMethod]
        public void ShouldApplyFlatHealth()
        {
            const int playerBonusHealthRegeneration = 10;
            var p = new Player();
            p.AddModifier(new HealthRegenerationAmount(10));
            Assert.IsTrue(p.BonusHealthRegeneration == playerBonusHealthRegeneration);
        }
    }
}
