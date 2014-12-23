using System;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.SkillTree.Paths.FighterPath;
using GameLogic.SkillTree.Paths.FighterPath.BarbarianPath;
using GameLogic.SkillTree.Paths.FighterPath.KnightPath;
using GameLogic.SkillTree.Paths.WizardPath;
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
            p.ChooseSkill(new PathOfTheFighter());
            p.ChooseSkill(new PathOfTheFighter());
        }

        [TestMethod]
        public void ShouldAddSkillPointEqualToPlayerLevel()
        {
            var p = new Player();
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
            p.ChooseSkill(new PathOfTheDarkKnight());
        }

        [TestMethod]
        public void ShouldAddModifiersToCharacterWhenSkillChosen()
        {
            var p = new Player();
            Assert.IsTrue(p.CharacterModifiers.Count == 0);
            p.LevelUp();
            p.ChooseSkill(new PathOfTheFighter());
            Assert.IsTrue(p.CharacterModifiers.Count > 0);
        }

        [TestMethod]
        public void ShouldAddActionsToCharacterWhenSkillChosen()
        {
            var p = new Player();
            p.LevelUp();
            Assert.IsFalse(p.GetActions(false).Exists(i => i is SpellSpear));
            p.ChooseSkill(new PathOfTheWizard());
            Assert.IsTrue(p.GetActions(false).Exists(i => i is SpellSpear));
        }

        [TestMethod]
        public void ShouldTakeSecondSkillIfParentTaken()
        {
            var p = new Player();
            p.SetLevel(2);
            p.ChooseSkill(new PathOfTheFighter());
            p.ChooseSkill(new PathOfTheKnight());
            Assert.IsTrue(p.CurrentClass == SkillBranches.Knight);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldNotBeAbleToTakeSkillAtLevelAbove()
        {
            var p = new Player();
            p.ChooseSkill(new PathOfTheKnight());
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldNotBeAbleToTakeMultipleTopLevelPaths()
        {
            var p = new Player();
            p.SetLevel(2);
            p.ChooseSkill(new PathOfTheFighter());
            p.ChooseSkill(new PathOfTheWizard());
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void ShouldNotBeAbleToTakeSkillWithoutParent()
        {
            var p = new Player();
            p.SetLevel(2);
            p.ChooseSkill(new PathOfTheBarbarian());
        }
    }
}
