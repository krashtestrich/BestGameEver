using System.Linq;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Characters.Player;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ShopTests
{
    [TestClass]
    public class WhenSellingEquipment
    {
        [TestMethod]
        public void ShouldGetCashBack()
        {
            const int expectedCash = 145;
            var p = new Player();
            var sword = new Sword();
            EquipmentHelper.EquipEquipment(p, sword);
            EquipmentHelper.SellEquipment(p, p.CharacterEquipment.First(i => i is Sword));
            Assert.IsTrue(p.Cash == expectedCash);
        }
    }
}
