using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Enums;
using GameLogic.SkillTree.Paths.FighterPath;
using GameLogic.SkillTree.Paths.WizardPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ShopTests
{
    [TestClass]
    public class WhenBotsShopping
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

        [TestMethod]
        public void FighterBotShouldPurchaseFighterItems()
        {
            var b = new Dumbass();
            b.AddSkillPoints(1);
            b.ChooseSkill(new PathOfTheFighter());
            b.AddCash(100);
            b.BuyItems();
            Assert.IsTrue(b.CharacterEquipment.TrueForAll(i => i.EquipmentSubTypes.Exists(j => j == EquipmentSubType.DefensiveFighter || j == EquipmentSubType.OffensiveFighter)));
        }
    }
}
