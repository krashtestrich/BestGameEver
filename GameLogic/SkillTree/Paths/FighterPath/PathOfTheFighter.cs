using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;

namespace GameLogic.SkillTree.Paths.FighterPath
{
    public class PathOfTheFighter : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Fighter"; }
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
                return SkillBranches.Fighter;
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
                    new HealthBonusAmount(50)    
                }; 
            }
        }
    }
}
