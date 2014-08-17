using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Mana;

namespace GameLogic.SkillTree.Mana
{
    public class ManaLevelOne : SkillBase
    {
        public override string Name
        {
            get { return "Mana Level 1"; }
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
            get { return SkillBranches.Mana; }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new LittleBitMoreMana()
                };
            }
        }
    }
}
