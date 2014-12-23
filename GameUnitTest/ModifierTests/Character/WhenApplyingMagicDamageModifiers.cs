using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.MagicDamage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingMagicDamageModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageMagicDamage()
        {
            var p = new Player();
            p.AddModifier(new MagicDamagePercentage(50));
            Assert.IsTrue(p.MagicDamageBonusPercent == 50);
        }

        [TestMethod]
        public void ShouldApplyMagicDamage()
        {
            var p = new Player();
            p.AddModifier(new MagicDamageAmount(10));
            Assert.IsTrue(p.MagicDamage == 10);
        }
    }
}
