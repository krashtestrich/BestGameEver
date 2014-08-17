using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Actions.Movements;
using GameLogic.Actions.Spells.Heals;
using GameLogic.Arena;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;
using GameLogic.Modifiers.Character.Mana;
using GameLogic.SkillTree;
using GameLogic.Slots;
using MoreLinq;

namespace GameLogic.Characters
{
    public abstract class Character : ICharacter, IGameEntity
    {
        #region Name

        public void SetName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        #endregion

        #region Health
        private int _currentHealth;

        public int BonusHealth { get; protected set; }

        public void AddBonusHealth(int amount)
        {
            BonusHealth += amount;
        }
        public int Health
        {
            get { return _currentHealth; }
        }

        public abstract int BaseHealth { get; }

        public void SetHealth()
        {
            BonusHealth = 0;
            foreach (var m in _modifiers.Where(i => i is HealthBase))
            {
                m.Apply(this);
            }
            _currentHealth = BonusHealth + BaseHealth;
        }

        public void LoseHealth(int amount)
        {
            _currentHealth = Health - amount;
        }

        public void GainHealth(int amount)
        {
            _currentHealth = Health + amount;
        }

        #endregion

        #region Mana
        private int _currentMana;

        public int BonusMana { get; protected set; }

        public void AddBonusMana(int amount)
        {
            BonusMana += amount;
        }
        public int Mana
        {
            get { return _currentMana; }
        }

        public abstract int BaseMana { get; }

        public void SetMana()
        {
            BonusMana = 0;
            foreach (var m in _modifiers.Where(i => i is ManaBase))
            {
                m.Apply(this);
            }
            _currentMana = BonusMana + BaseMana;
        }

        public void LoseMana(int amount)
        {
            _currentMana = Mana - amount;
        }

        public void GainMana(int amount)
        {
            _currentMana = Mana + amount;
        }
        #endregion

        #region Cash
        public int Cash { get; protected set; }

        public void SetCash(int amount)
        {
            Cash = amount;
        }

        public void AddCash(int amount)
        {
            Cash += amount;
        }
        #endregion

        #region Slots
        private readonly List<Slot> _slots;

        public List<Slot> Slots
        {
            get
            {
                return _slots;
            }
        }

        public void AddSlot(Slot slot)
        {
            _slots.Add(slot);
        }
        #endregion

        #region Position

        public ArenaFloorTile ArenaLocation { get; private set; }
        public ArenaFloorTile TargettedTile { get; private set; }

        public void SetEntityLocation(ArenaFloorTile tile)
        {
            ArenaLocation = tile;
        }
        #endregion

        #region Equipment
        private readonly List<Equipment.Equipment> _characterEquipment;

        public bool CanEquipEquipment(Equipment.Equipment equipment)
        {
            var uniqueSlots = equipment.Slots.Distinct();

            foreach (var e in uniqueSlots)
            {
                if (_slots.Count(i => i.SlotFree && i.SlotType == e.SlotType) < equipment.Slots.Select(x => x.SlotType == e.SlotType).Count())
                {
                    return false;
                }
            }
            return true;
        }

        public void EquipEquipment(Equipment.Equipment equipment)
        {
            if (CanEquipEquipment(equipment))
            {
                _characterEquipment.Add(equipment);
                foreach (var s in equipment.Slots)
                {
                    _slots.Find(sf => sf.SlotFree && sf.SlotType == s.SlotType).SetSlotFree(false, equipment.Name);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public void UnEquipEquipment(Equipment.Equipment equipment)
        {
            _characterEquipment.Remove(equipment);
            foreach (var s in equipment.Slots)
            {
                _slots.Find(sf => !sf.SlotFree && sf.SlotType == s.SlotType).SetSlotFree(true, null);
            }
        }

        public List<IAction> CurrentAvailableActions { get; private set; }

        public List<Equipment.Equipment> CharacterEquipment
        {
            get
            {
                return _characterEquipment;
            }
        }

        #endregion    

        #region Alliance
        private Alliance _alliance;

        public void ChangeAlliance(Alliance a)
        {
            _alliance = a;
        }

        public Alliance GetAlliance()
        {
            return _alliance;
        }
        #endregion

        #region Actions

        private readonly List<IAction> _nativeActions;

        public List<IAction> TargetTileAndSelectActions(ArenaFloorTile tile)
        {
            TargetTile(tile);
            CurrentAvailableActions = GetActions(true);
            return CurrentAvailableActions;
        }

        public List<IAction> GetActions(bool canPerform)
        {
            return _nativeActions.Concat(CharacterEquipment.SelectMany(i => i.Actions).ToList().DistinctBy(i => i.Name)).ToList().Where(i => !canPerform || i.CanBePerformed(this)).ToList();
        }

        public void UntargetTile()
        {
            TargettedTile = null;
            CurrentAvailableActions = null;
        }

        public void TargetTile(ArenaFloorTile tile)
        {
            TargettedTile = tile;
        }

        #endregion

        #region Damage/Block Calculations

        private IEnumerable<int> GetBlockModifiers()
        {
            return CharacterEquipment.Where(i => i is Shield).Select(i => ((Shield) i).GetBlock());
        }

        private int CalculateDamageTaken(int damage)
        {
            var blockModifiers = GetBlockModifiers();
            blockModifiers.ToList().ForEach(i => damage = damage - (damage * i/100));
            return damage;
        }

        public void TakeDamage(int damage)
        {
            LoseHealth(CalculateDamageTaken(damage));
        }

        public void TakeSpellDamage(int damage)
        {
            LoseHealth(damage);
        }

        public void ReceiveHeal(int heal)
        {
            GainHealth(heal);
        }
        #endregion

        #region Modifiers
        private readonly List<IModifier<ICharacter>> _modifiers;

        public List<IModifier<ICharacter>> CharacterModifiers
        {
            get { return _modifiers; }
        }

        public void AddModifier(IModifier<ICharacter> modifier)
        {
            if (!_modifiers.Exists(i => i.Name ==  modifier.Name))
            {
                modifier.Apply(this);
                _modifiers.Add(modifier);
            }
        }

        public void RemoveModifier(IModifier<ICharacter> modifier)
        {
            var existingModifier = _modifiers.FirstOrDefault(i => i.Name == modifier.Name);
            if (existingModifier != null)
            {
                _modifiers.Remove(existingModifier);
            }
        }
        #endregion

        #region Level
        private int _level;

        public int GetLevel()
        {
            return _level;
        }

        public virtual void SetLevel(int level)
        {
            _level = level;
            var levelsToAdd = 0;
            while (level > 0)
            {
                levelsToAdd += level;
                level--;
            }
            AddSkillPoints(levelsToAdd);
        }

        public virtual void LevelUp()
        {
            _level++;
            AddSkillPoints(_level);
        }
        #endregion

        #region Skill Points

        private void AddSkillPoints(int points)
        {
            SkillPoints += points;
        }

        public int SkillPoints { get; private set; }
        #endregion

        #region Skill Tree

        private readonly SkillTree.SkillTree _skillTree;
        public SkillTree.SkillTree SkillTree
        {
            get { return _skillTree; }
        }

        public void ChooseSkill(SkillBase skill)
        {
            if (skill.Cost > SkillPoints)
            {
                throw new Exception("You cannot afford this skill.");
            }
            _skillTree.TakeSkill(skill, this);
            SkillPoints -= skill.Cost;
        }

        public void UnchooseSkill(SkillBase skill)
        {
            _skillTree.CancelSkill(skill, this);
        }

        #endregion

        #region Arena / Game
        public void LeaveArena()
        {
            SetHealth();
            SetEntityLocation(null);
        }
        #endregion

        #region Shop Stuff

        public virtual void BuyItems()
        {
            
        }

        public bool CanAffordEquipment(Equipment.Equipment e)
        {
            return (Cash >= e.Price);
        }

        public void PurchaseEquipment(Equipment.Equipment e)
        {
            if (CanAffordEquipment(e) && CanEquipEquipment(e))
            {
                Cash -= e.Price;
                EquipEquipment(e);
            }
            else
            {
                throw new Exception("Player tried to purchase unaffordable or unwearable Equipment.");
            }
        }

        public void SellEquipment(Equipment.Equipment e)
        {
            Cash += (int)Math.Round(e.Price * 0.75, MidpointRounding.ToEven);
            UnEquipEquipment(e);
        }
        #endregion

        protected Character()
        {
            _modifiers = new List<IModifier<ICharacter>>();
            SetHealth();
            SetMana();
            _characterEquipment = new List<Equipment.Equipment>();
            _skillTree = new SkillTree.SkillTree();

            _slots = new List<Slot>();

            var h1 = new Hand();
            _slots.Add(h1);

            var h2 = new Hand();
            _slots.Add(h2);

            _nativeActions = new List<IAction>
            {
                new Run(),
                new LittleHeal(),
                new BigHeal()
            };
        }
    }
}
