using System.Collections.Generic;
using System.Linq;
using GameLogic.AI;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Helpers;
using GameLogic.SkillTree.Paths;

namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character, IBot
    {
        protected Bot()
        {
            SetName(BotHelper.GenerateRandomBotName());
        }

        public abstract int Worth { get; }

        #region Skill Tree

        private void AssignSkillTreePoints()
        {
            var points = SkillPoints;
            var availableSkills = GetAvailableSkillNodes(points);
            if (availableSkills == null || availableSkills.Count <= 0)
            {
                return;
            }
            ChooseSkill(availableSkills[new ThreadSafeRandom().Next(0, availableSkills.Count)]);
        }

        private List<IPath> GetAvailableSkillNodes(int maxCost)
        {
            var skillTree = SkillTree.Get();
            var currentSkills = skillTree.Where(i => i.IsActive).ToList();
            var highestSkill = currentSkills
                .OrderByDescending(i => i.Level)
                .FirstOrDefault();
            var mainPath = highestSkill != null
                ? highestSkill.BasePath
                : SkillBranches.NotTaken;
            var level = highestSkill != null
                ? highestSkill.Level
                : 0;
            
            return skillTree
                .Where(i => 
                            (mainPath == SkillBranches.NotTaken || i.BasePath == mainPath) 
                            && i.Level > level
                            && !i.IsActive 
                            && i.Cost <= maxCost 
                            && (i.Parent == null || currentSkills
                                        .Exists(p => p.Name == i.Parent.Name)))
                .OrderByDescending(i => i.Level)
                .ToList();
        }

        #endregion

        #region Levels
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

        #endregion

        #region Shop
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

        #endregion

        #region Action Choices

        public void PreferredActions()
        {
            var actionType = ActionChoosers.GetPreferredActionType(this);
        }
        #endregion
    }
}
