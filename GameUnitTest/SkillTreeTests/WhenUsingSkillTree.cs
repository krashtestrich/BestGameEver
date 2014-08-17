using System;
using GameLogic.Characters.Player;
using GameLogic.SkillTree.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.SkillTreeTests
{
    [TestClass]
    public class WhenUsingSkillTree
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotBeAbleToTakeSkillAlreadyTaken()
        {
            var p = new Player();
            p.ChooseSkill(new HealthLevelOne());
            p.ChooseSkill(new HealthLevelOne());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotBeAbleToUnchooseInactiveSkill()
        {
            var p = new Player();
            p.UnchooseSkill(new HealthLevelOne());
        }

        [TestMethod]
        public void ShouldAddSkillPointsWhenLevelUp()
        {
            var p = new Player();
            Assert.IsTrue(p.SkillPoints == 0);
            p.LevelUp();
            Assert.IsTrue(p.SkillPoints == 1);
        }

        [TestMethod]
        public void ShouldAddSkillPointEqualToPlayerLevel()
        {
            var p = new Player();
            p.LevelUp();
            Assert.IsTrue(p.SkillPoints == 1);
            p.LevelUp();
            Assert.IsTrue(p.SkillPoints == 3);
        }

        [TestMethod]
        public void ShouldAddSkillPointsWhenLevelAssigned()
        {
            var p = new Player();
            p.SetLevel(3);
            Assert.IsTrue(p.SkillPoints == 6);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotTakeSkillIfCharacterCannotAfford()
        {
            var p = new Player();
            p.ChooseSkill(new HealthLevelOne());
        }

        [TestMethod]
        public void ShouldAddModifiersToCharacterWhenSkillChosen()
        {
            var p = new Player();
            Assert.IsTrue(p.CharacterModifiers.Count == 0);
            p.LevelUp();
            p.ChooseSkill(new HealthLevelOne());
            Assert.IsTrue(p.CharacterModifiers.Count > 0);
        }

        [TestMethod]
        public void ShouldRemoveModifiersToCharacterWhenSkillUnchosen()
        {
            var p = new Player();
            p.SetLevel(2);
            p.ChooseSkill(new HealthLevelOne());
            Assert.IsTrue(p.CharacterModifiers.Count > 0);
            p.UnchooseSkill(new HealthLevelOne());
            Assert.IsTrue(p.CharacterModifiers.Count == 0);
        }

        [TestMethod]
        public void ShouldTakeSecondSkillIfParentTaken()
        {
            var p = new Player();
            p.SetLevel(2);
            p.ChooseSkill(new HealthLevelOne());
            p.ChooseSkill(new HealthLevelTwo());
            Assert.IsTrue(p.CharacterModifiers.Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotBeAbleToTakeSkillAtLevelAbove()
        {
            var p = new Player();
            p.ChooseSkill(new HealthLevelTwo());
        }
    }
}
