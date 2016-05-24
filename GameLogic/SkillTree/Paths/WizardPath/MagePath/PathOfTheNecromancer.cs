using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Mana;

namespace GameLogic.SkillTree.Paths.WizardPath.MagePath
{
    public class PathOfTheNecromancer : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Necromancer"; }
        }

        public int Cost
        {
            get { return 1; }
        }

        public int Level
        {
            get { return 3; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.Necromancer;
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
                    new ManaBonusPercentage(15)
                };
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Wizard; }
        }
    }
}
