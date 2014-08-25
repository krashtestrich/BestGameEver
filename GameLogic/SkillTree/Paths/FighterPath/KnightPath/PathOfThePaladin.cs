using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;

namespace GameLogic.SkillTree.Paths.FighterPath.KnightPath
{
    public class PathOfThePaladin : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Paladin"; }
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
                return SkillBranches.Paladin;
            }
        }

        public override IPath Parent
        {
            get
            {
                return new PathOfTheKnight();
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
            get { return SkillBranches.Fighter; }
        }
    }
}
