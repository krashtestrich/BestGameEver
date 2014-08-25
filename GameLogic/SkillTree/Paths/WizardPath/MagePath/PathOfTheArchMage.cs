using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;

namespace GameLogic.SkillTree.Paths.WizardPath.MagePath
{
    public class PathOfTheArchMage : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the ArchMage"; }
        }

        public int Cost
        {
            get { return 3; }
        }

        public int Level
        {
            get { return 3; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.ArchMage;
            }
        }

        public override IPath Parent
        {
            get
            {
                return new PathOfTheMage();
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
