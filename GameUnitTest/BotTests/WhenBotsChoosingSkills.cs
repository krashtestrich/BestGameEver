using System.Linq;
using GameLogic.Characters.Bots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BotTests
{
    [TestClass]
    public class WhenBotsChoosingSkills
    {
        [TestMethod]
        public void ShouldChooseSkillOnLevelUp()
        {
            var b = new Dumbass();
            b.LevelUp();
            Assert.IsTrue(b.SkillTree.Get().Where(i => i.IsActive).ToList().Count == 1);
        }

        [TestMethod]
        public void ShouldChooseMultipleSkillsWhenLevelUpTwice()
        {
            var b = new Dumbass();
            b.LevelUp();
            Assert.IsTrue(b.SkillTree.Get().Where(i => i.IsActive).ToList().Count == 1);
            b.LevelUp();
            Assert.IsTrue(b.SkillTree.Get().Where(i => i.IsActive).ToList().Count == 2);
        }
    }
}
