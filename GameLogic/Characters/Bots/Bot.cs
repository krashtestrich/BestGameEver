using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Equipment.Shields;
using GameLogic.Equipment.Weapons;
using GameLogic.Helpers;
using GameLogic.SkillTree.Paths;

namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character
    {
        protected Bot()
        {
            SetName(BotHelper.GenerateRandomBotName());
        }

        public int Worth
        {
            get { return 50; }
        }

        public override int BaseHealth
        {
            get { return 100; }
        }

        public override int BaseMana
        {
            get { return 0; }
        }

        public override int BaseHealthRegeneration
        {
            get { return 1; }
        }

        #region Skill Tree

        private void AssignSkillTreePoints()
        {
            var points = SkillPoints;
            var availableSkills = GetAvailableSkillNodes(points);
            if (availableSkills == null || availableSkills.Count <= 0)
            {
                return;
            }
            ChooseSkill(availableSkills[SecureRandom.Next(0, availableSkills.Count)]);
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
            var weapon = _equipmentBuyersByClass.ContainsKey(CurrentClass.GetValueOrDefault())
                ? _equipmentBuyersByClass[CurrentClass.GetValueOrDefault()].Invoke(this, affordableWeapons)
                : _equipmentBuyersByClass[CurrentPath.BasePath].Invoke(this, affordableWeapons);
            EquipmentHelper.PurchaseEquipment(this, weapon);

            if (weapon.Slots.Count == 1)
            {
                var affordableShield = s.Equipment.Where(i => i is Shield && i.Price <= Cash).ToList();
                var shield = _equipmentBuyersByClass.ContainsKey(CurrentClass.GetValueOrDefault())
                ? _equipmentBuyersByClass[CurrentClass.GetValueOrDefault()].Invoke(this, affordableShield)
                : _equipmentBuyersByClass[CurrentPath.BasePath].Invoke(this, affordableShield);
                EquipmentHelper.PurchaseEquipment(this, shield);
            }
        }

        private readonly Dictionary<SkillBranches, Func<ICharacter, List<IBuyableEquipment>, IBuyableEquipment>> _equipmentBuyersByClass = 
            new Dictionary<SkillBranches, Func<ICharacter, List<IBuyableEquipment>, IBuyableEquipment>>
            {
                {
                     SkillBranches.Wizard, (c, e) =>
                     {
                         if (!c.CharacterEquipment.Exists(i => i is Weapon))
                         {
                             var equip = e.Where(i => i.EquipmentType == EquipmentType.OneHandedWeapon
                                                      && i.EquipmentSubTypes.IndexOf(EquipmentSubType.Caster) > -1).ToList();
                             return equip[SecureRandom.Next(0, equip.Count)];
                         }
                         else
                         {
                             var equip = e.Where(i => i.EquipmentType == EquipmentType.Shield
                                                      && i.EquipmentSubTypes.IndexOf(EquipmentSubType.Caster) > -1).ToList();
                             return equip[SecureRandom.Next(0, equip.Count)];
                         }
                     }
                 },
                 {
                    SkillBranches.Fighter, (c, e) =>
                        {
                            if (!c.CharacterEquipment.Exists(i => i is Weapon))
                            {
                                var equip = e.Where(i => i.EquipmentType == EquipmentType.OneHandedWeapon || i.EquipmentType == EquipmentType.TwoHandedWeapon
                                                         && (i.EquipmentSubTypes.IndexOf(EquipmentSubType.DefensiveFighter) > -1
                                                            || i.EquipmentSubTypes.IndexOf(EquipmentSubType.OffensiveFighter) > -1)).ToList();
                                return equip[SecureRandom.Next(0, equip.Count)];
                            }
                            else
                            {
                                var equip = e.Where(i => i.EquipmentType == EquipmentType.Shield
                                                        && (i.EquipmentSubTypes.IndexOf(EquipmentSubType.DefensiveFighter) > -1
                                                            || i.EquipmentSubTypes.IndexOf(EquipmentSubType.OffensiveFighter) > -1)).ToList();
                                return equip[SecureRandom.Next(0, equip.Count)];
                            }
                        } 
                    }
            }; 

        #endregion
        
    }
}
