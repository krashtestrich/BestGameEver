using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;

namespace GameLogic.SkillTree.Health
{
    public class HealthLevelTwo : SkillBase
    {
        public override string Name
        {
            get { return "Health Level Two"; }
        }

        public override int Cost
        {
            get { return 2; }
        }

        public override int Level
        {
            get { return 2; }
        }

        public override SkillBase Parent
        {
            get
            {
                return new HealthLevelOne();
            }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new SuperHealth()
                };
            }
        }

        public override SkillBranches Branch
        {
            get { return SkillBranches.Health; }
        }
    }
}
