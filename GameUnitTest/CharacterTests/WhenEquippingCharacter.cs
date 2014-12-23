using System;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Characters.Player;
using GameLogic.Equipment.Shields;
using GameLogic.Modifiers.Character.MagicDamage;
using GameLogic.SkillTree.Paths.WizardPath;
using GameLogic.Slots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.CharacterTests
{
    [TestClass]
    public class WhenEquippingCharacter
    {
        [TestMethod]
        public void ShouldEquipEquipmentIfCharacterHasFreeSlots()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            Assert.IsTrue(EquipmentHelper.CanEquipEquipment(c, e));
        }

        [TestMethod]
        public void ShouldEquipEquipmentIfCharacterDoesNotHaveEnoughFreeSlots()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            e.AddSlotType(new Hand());
            e.AddSlotType(new Hand());
            e.AddSlotType(new Hand());
            Assert.IsFalse(EquipmentHelper.CanEquipEquipment(c, e));
        }

        [TestMethod]
        public void ShouldAddEquipmentToCharactersEquipment()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            EquipmentHelper.EquipEquipment(c, e);
            Assert.IsTrue(c.CharacterEquipment.Exists(x => x == e));
        }

        [TestMethod]
        public void ShouldUpdatesCharacterEquipmentSlotsToUsed()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            e.AddSlotType(new Hand());
            EquipmentHelper.EquipEquipment(c, e);
            Assert.IsTrue(c.Slots.Exists(x => !x.SlotFree && x.SlotEquipmentName == e.Name));
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldThrowExceptionWhenCharacterDoesNotHaveEnoughFreeSlots()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            e.AddSlotType(new Hand());
            e.AddSlotType(new Hand());
            e.AddSlotType(new Hand());
            EquipmentHelper.EquipEquipment(c, e);
        }

        [TestMethod]
        public void ShouldRemovesEquipmentFromCharactersEquipment()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            EquipmentHelper.EquipEquipment(c, e);
            EquipmentHelper.UnEquipEquipment(c, e);
            Assert.IsFalse(c.CharacterEquipment.Exists(x => x == e));
        }

        [TestMethod]
        public void ShouldFreesCharactersEquipmentSlots()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            EquipmentHelper.EquipEquipment(c, e);
            EquipmentHelper.UnEquipEquipment(c, e);
            Assert.IsFalse(c.Slots.Exists(x => !x.SlotFree || x.SlotEquipmentName == e.Name));
        }

        [TestMethod]
        public void ShouldCalculateWeaponDamage()
        {
            var w = new TestHelpers.TestWeapon();
            var dmg = w.GetDamage();
            Assert.IsTrue((w.BaseDamage + w.BonusDamage) >= dmg && dmg >= w.BaseDamage);
        }

        [TestMethod]
        public void ShouldApplyEquipmentModifiersToCharacter()
        {
            var c = new Player();
            c.LevelUp();
            c.ChooseSkill(new PathOfTheWizard());
            var e = new CastersShield();
            EquipmentHelper.EquipEquipment(c, e);
            Assert.IsTrue(c.CharacterModifiers.Exists(i => i is MagicDamagePercentage));
        }
    }
}
