using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;

namespace GameLogic.SkillTree.Health
{
    public class HealthLevelOne : SkillBase
    {
        public override string Name
        {
            get { return "Health Level 1"; }
        }

        public override int Cost
        {
            get { return 1; }
        }

        public override int Level
        {
            get { return 0; }
        }

        public override SkillBranches Branch
        {
            get { return SkillBranches.Health; }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get 
            { 
                return new List<IModifier<ICharacter>>
                {
                    new WeeBitOfHealth()    
                }; 
            }
        }
    }
}
