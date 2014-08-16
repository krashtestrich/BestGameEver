using System;
using GameLogic.Characters.Player;
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
            Assert.IsTrue(c.CanEquipEquipment(e));
        }

        [TestMethod]
        public void ShouldEquipEquipmentIfCharacterDoesNotHaveEnoughFreeSlots()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            e.AddSlotType(new Hand());
            e.AddSlotType(new Hand());
            e.AddSlotType(new Hand());
            Assert.IsFalse(c.CanEquipEquipment(e));
        }

        [TestMethod]
        public void ShouldAddEquipmentToCharactersEquipment()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            c.EquipEquipment(e);
            Assert.IsTrue(c.CharacterEquipment.Exists(x => x == e));
        }

        [TestMethod]
        public void ShouldUpdatesCharacterEquipmentSlotsToUsed()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            e.AddSlotType(new Hand());
            c.EquipEquipment(e);
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
            c.EquipEquipment(e);
        }

        [TestMethod]
        public void ShouldRemovesEquipmentFromCharactersEquipment()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            c.EquipEquipment(e);
            c.UnEquipEquipment(e);
            Assert.IsFalse(c.CharacterEquipment.Exists(x => x == e));
        }

        [TestMethod]
        public void ShouldFreesCharactersEquipmentSlots()
        {
            var c = new Player();
            var e = new TestHelpers.TestWeapon();
            c.EquipEquipment(e);
            c.UnEquipEquipment(e);
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
        public void ShouldCalculateAttackActionDamage()
        {
            
        }
    }
}
