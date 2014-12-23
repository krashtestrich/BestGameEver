using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.PhysicalDamage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingPhysicalDamageModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentagePhysicalDamage()
        {
            var p = new Player();
            p.AddModifier(new PhysicalDamagePercentage(50));
            Assert.IsTrue(p.PhysicalDamageBonusPercent == 50);
        }

        [TestMethod]
        public void ShouldApplyPhysicalDamage()
        {
            var p = new Player();
            p.AddModifier(new PhysicalDamageAmount(10));
            Assert.IsTrue(p.PhysicalDamage == 10);
        }
    }
}
