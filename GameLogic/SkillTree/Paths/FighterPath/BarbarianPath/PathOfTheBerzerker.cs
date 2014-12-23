using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;
using GameLogic.Modifiers.Character.PhysicalDamage;

namespace GameLogic.SkillTree.Paths.FighterPath.BarbarianPath
{
    public class PathOfTheBerzerker : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Berzerker"; }
        }

        public override IPath Parent
        {
            get
            {
                return new PathOfTheBarbarian();
            }
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
                return SkillBranches.Berzerker;
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Fighter; }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new HealthBonusPercentage(50),
                    new HealthBonusAmount(20),
                    new PhysicalDamagePercentage(20),
                    new PhysicalDamageAmount(10)
                };
            }
        }
    }
}
