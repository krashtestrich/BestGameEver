using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions.Attacks;
using GameLogic.Actions.Spells;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Actions.Spells.Heals;
using GameLogic.Characters;
using GameLogic.Enums;

namespace GameLogic.AI
{
    public class ActionChoosers
    {
        private static readonly Dictionary<SkillBranches, Func<ICharacter, Type>> PreferredActionTypeBySkillPath = new Dictionary<SkillBranches, Func<ICharacter, Type>>
        {
            {
                SkillBranches.Fighter, c => typeof (AttackBase)
            },
            {
                SkillBranches.Wizard, c =>
                {
                    var spells =
                        c.GetActions(false).Where(i => i is ISpell).Cast<ISpell>().Where(i => i.ManaCost <= c.Mana).ToList();
                    if (!spells.Any())
                    {
                        return typeof (AttackBase);
                    }
                    if (c.Health < ((c.BonusHealth + c.BaseHealth)/2) && spells.Exists(i => i is HealBase))
                    {
                        return typeof (HealBase);
                    }
                    return spells.Exists(i => i is DamageBase) ? typeof (DamageBase) : typeof (AttackBase);
                }
            }
        }; 
        public static Type GetPreferredActionType(ICharacter c)
        {
            var highestPath = c.SkillTree.Get().Where(i => i.IsActive).OrderByDescending(i => i.Level).First();
            
            //TODO: Add different Action chooser for each class type.
            return
                PreferredActionTypeBySkillPath.ContainsKey(highestPath.Path)
                    ? PreferredActionTypeBySkillPath[highestPath.Path].Invoke(c)
                    : PreferredActionTypeBySkillPath[highestPath.BasePath].Invoke(c);
        }
    }
}
