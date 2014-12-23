using System.Collections.Generic;
using GameLogic.Actions.Attacks;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;
using GameLogic.Modifiers.Character.PhysicalDamage;

namespace GameLogic.SkillTree.Paths.FighterPath.BarbarianPath
{
    public class PathOfTheBarbarian : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Barbarian"; }
        }

        public override IPath Parent
        {
            get
            {
                return new PathOfTheFighter();
            }
        }

        public int Cost
        {
            get { return 1; }
        }

        public int Level
        {
            get { return 2; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.Barbarian;
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Fighter; }
        }

        public PathOfTheBarbarian()
        {
            //Actions.Add(new RecklessSwing());
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new HealthBonusPercentage(25),
                    new PhysicalDamagePercentage(30)
                };
            }
        }
    }
}
