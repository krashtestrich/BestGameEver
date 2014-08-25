using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Actions.Spells.Heals;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Mana;

namespace GameLogic.SkillTree.Paths.WizardPath
{
    public class PathOfTheWizard : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Wizard"; }
        }

        public int Cost
        {
            get { return 1; }
        }

        public int Level
        {
            get { return 1; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.Wizard;
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Wizard; }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get 
            {
                return new List<IModifier<ICharacter>>
                {
                    new ManaBonusAmount(50)
                };
            }
        }

        public override List<IAction> Actions
        {
            get
            {
                return new List<IAction>
                {
                    new LittleHeal(),
                    new SpellSpear()
                };
            }
        }
    }
}
