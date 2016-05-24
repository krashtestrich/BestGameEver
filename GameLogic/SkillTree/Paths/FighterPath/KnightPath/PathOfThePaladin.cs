using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Armor;
using GameLogic.Modifiers.Character.Mana;

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
                    new ManaBonusAmount(50),
                    new ArmorBonusPercentage(15)
                };
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Fighter; }
        }
    }
}
