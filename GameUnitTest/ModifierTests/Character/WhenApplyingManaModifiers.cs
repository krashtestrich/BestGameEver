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
            p.AddModifier(new TastyMana());
            Assert.IsTrue(p.BonusMana == 50);
        }

        [TestMethod]
        public void ShouldApplyFlatHealth()
        {
            var p = new Player();
            p.AddModifier(new LittleBitMoreMana());
            Assert.IsTrue(p.BonusMana == 10);
        }
    }
}
