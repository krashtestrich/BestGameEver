using System.Collections.Generic;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Mana;

namespace GameLogic.SkillTree.Paths.WizardPath.MagePath
{
    public class PathOfTheMage : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Mage"; }
        }

        public int Cost
        {
            get { return 2; }
        }

        public int Level
        {
            get { return 2; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.Mage;
            }
        }

        public override IPath Parent
        {
            get
            {
                return new PathOfTheWizard();
            }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new ManaBonusPercentage(50),
                };
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Wizard; }
        }

        public PathOfTheMage()
        {
            Actions.Add(new MindBlast());
        }
    }
}
