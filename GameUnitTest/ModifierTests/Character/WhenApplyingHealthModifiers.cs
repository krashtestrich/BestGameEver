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
            var p = new Player();
            p.AddModifier(new HealthBonusPercentage(50));
            Assert.IsTrue(p.BonusHealth == 50);
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
