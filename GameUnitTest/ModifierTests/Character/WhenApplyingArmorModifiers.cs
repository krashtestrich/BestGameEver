using GameLogic.Characters.CharacterHelpers;
using GameLogic.Characters.Player;
using GameLogic.Equipment.Shields;
using GameLogic.Modifiers.Character.Armor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingArmorModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageArmor()
        {
            var p = new Player();
            EquipmentHelper.EquipEquipment(p, new BlessedCardboard());
            p.AddModifier(new ArmorBonusPercentage(50));
            Assert.IsTrue(p.BonusArmor == 2);
        }

        [TestMethod]
        public void ShouldApplyFlatArmor()
        {
            var p = new Player();
            p.AddModifier(new ArmorBonusAmount(10));
            Assert.IsTrue(p.BonusArmor == 10);
        }
    }
}
