using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.Block;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingBlockModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageBlock()
        {
            var p = new Player();
            p.AddModifier(new BlockPercentage(50));
            Assert.IsTrue(p.BonusBlockAmount == 50);
        }
    }
}
