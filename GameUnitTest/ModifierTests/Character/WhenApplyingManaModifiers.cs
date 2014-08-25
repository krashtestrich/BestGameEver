using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.Mana;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingManaModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageMana()
        {
            var p = new Player();
            p.GainMana(50);
            p.AddModifier(new ManaBonusPercentage(50));
            Assert.IsTrue(p.BonusMana == 25);
        }

        [TestMethod]
        public void ShouldApplyFlatMana()
        {
            var p = new Player();
            p.AddModifier(new ManaBonusAmount(10));
            Assert.IsTrue(p.BonusMana == 10);
        }
    }
}
