using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingHealthModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageHealth()
        {
            const int playerStartingHealth = 150;
            const int playerBonusHealth = 75;
            var p = new Player();
            p.AddModifier(new HealthBonusPercentage(50));
            Assert.IsTrue(p.BonusHealth == playerBonusHealth);
        }

        [TestMethod]
        public void ShouldApplyFlatHealth()
        {
            var p = new Player();
            p.AddModifier(new HealthBonusAmount(10));
            Assert.IsTrue(p.BonusHealth == 10); 
        }
    }
}
