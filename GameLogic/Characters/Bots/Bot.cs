using System.Collections.Generic;
using System.Linq;
using GameLogic.Equipment;
using GameLogic.Helpers;
using GameLogic.SkillTree;

namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character, IBot
    {
        protected Bot()
        {
            SetName(BotHelper.GenerateRandomBotName());
        }

        public abstract int Worth { get; }

        private void AssignSkillTreePoints()
        {
            var points = SkillPoints;
            var availableSkills = GetAvailableSkillNodes(points);
            if (availableSkills != null && availableSkills.Count > 0)
            {
               //TODO: Logic for this. For now bot just take highest skill it can.
                var highestLevel = availableSkills.First().Level;
                if (availableSkills.Count(i => i.Level == highestLevel) > 1)
                {
                    var choices = availableSkills.Where(i => i.Level == highestLevel).ToList();
                    ChooseSkill(choices[new ThreadSafeRandom().Next(0, choices.Count)]);
                }
                else
                {
                    ChooseSkill(availableSkills.First());
                }
            }
        }

        private List<SkillBase> GetAvailableSkillNodes(int maxCost)
        {
            var skillTree = SkillTree.Get();
            var currentSkills = skillTree.Where(i => i.IsActive);
            return skillTree
                .Where(i => !i.IsActive && i.Cost <= maxCost && (i.Parent == null || currentSkills
                    .ToList()
                    .Exists(p => p.Name == i.Parent.Name)))
                .OrderByDescending(i => i.Level).ToList();
        }

        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            AssignSkillTreePoints();
        }

        public override void LevelUp()
        {
            base.LevelUp();
            AssignSkillTreePoints();
        }

        public override void BuyItems()
        {
            var s = new Shop.Shop();
            var affordableWeapons = s.Equipment.Where(i => i is Weapon && i.Price <= Cash).ToList();
            var weapon =  affordableWeapons[new ThreadSafeRandom().Next(0, affordableWeapons.Count)];
            PurchaseEquipment(weapon);

            if (weapon.Slots.Count == 1)
            {
                var affordableShield = s.Equipment.Where(i => i is Shield && i.Price <= Cash).ToList();
                var shield = affordableShield[new ThreadSafeRandom().Next(0, affordableShield.Count)];
                PurchaseEquipment(shield);
            }
        }
    }
}
