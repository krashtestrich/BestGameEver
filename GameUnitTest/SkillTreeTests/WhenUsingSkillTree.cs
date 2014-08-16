using System;
using GameLogic.Characters.Player;
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
            p.ChooseSkill("Healthy Skill!");
            p.ChooseSkill("Healthy Skill!");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotBeAbleToUnchooseInactiveSkill()
        {
            var p = new Player();
            p.UnchooseSkill("Healthy Skill!");
        }

        [TestMethod]
        public void ShouldAddModifiersToCharacterWhenSkillChosen()
        {
            var p = new Player();
            Assert.IsTrue(p.CharacterModifiers.Count == 0);
            p.ChooseSkill("Healthy Skill!");
            Assert.IsTrue(p.CharacterModifiers.Count > 0);
        }

        [TestMethod]
        public void ShouldRemoveModifiersToCharacterWhenSkillUnchosen()
        {
            var p = new Player();
            p.ChooseSkill("Healthy Skill!");
            Assert.IsTrue(p.CharacterModifiers.Count > 0);
            p.UnchooseSkill("Healthy Skill!");
            Assert.IsTrue(p.CharacterModifiers.Count == 0);
        }
    }
}
