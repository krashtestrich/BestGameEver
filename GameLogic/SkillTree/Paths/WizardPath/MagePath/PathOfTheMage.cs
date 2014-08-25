using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;

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
                    
                };
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Wizard; }
        }
    }
}
