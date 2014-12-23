using GameLogic.Characters.Bots;
using GameLogic.Enums;
using GameLogic.SkillTree.Paths.WizardPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ShopTests
{
    [TestClass]
    public class WhenShopping
    {
        [TestMethod]
        public void WizardBotShouldPurchaseCasterItems()
        {
            var b = new Dumbass();
            b.AddSkillPoints(1);
            b.ChooseSkill(new PathOfTheWizard());
            b.AddCash(100);
            b.BuyItems();
            Assert.IsTrue(b.CharacterEquipment.TrueForAll(i => i.EquipmentSubTypes.Contains(EquipmentSubType.Caster)));
        }
    }
}
