using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Mana;

namespace GameLogic.SkillTree.Mana
{
    public class ManaLevelTwo : SkillBase
    {
        public override string Name
        {
            get { return "Mana Level Two!"; }
        }

        public override int Cost
        {
            get { return 2; }
        }

        public override int Level
        {
            get { return 2; }
        }

        public override SkillBranches Branch
        {
            get { return SkillBranches.Mana; }
        }

        public override SkillBase Parent
        {
            get { return new ManaLevelOne(); }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get 
            { 
                return new List<IModifier<ICharacter>>
                {
                    new TastyMana()
                }; 
            }
        }
    }
}
