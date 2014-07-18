using GameLogic.Characters.Player;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest
{
    [TestClass]
    public class WhenCreatingPlayer
    {
        [TestMethod]
        public void ShouldSetCash()
        {
            var p = new Player();
            var startingCash = p.Cash;
            p.SetCash(startingCash + 100);
            Assert.IsTrue(p.Cash == (startingCash + 100));
        }

        [TestMethod]
        public void ShouldAddCash()
        {
            var p = new Player();
            var startingCash = p.Cash;
            p.AddCash(10);
            Assert.IsTrue(p.Cash == (startingCash + 10));
        }

        [TestMethod]
        public void ShouldAffordAffordableEquipment()
        {
            var p = new Player();
            var s = new Sword();
            if (p.Cash < s.Price)
            {
                p.AddCash(s.Price);
            }
            Assert.IsTrue(p.CanAffordEquipment(s));
        }

        [TestMethod]
        public void ShouldNotAffordUnaffordableEquipment()
        {
            var p = new Player();
            var s = new Sword();
            if (p.Cash >= s.Price) 
            {
                p.SetCash(s.Price-100);
            }
            Assert.IsFalse(p.CanAffordEquipment(s));
        }
    }
}
