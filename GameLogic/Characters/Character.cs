using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Actions.Movements;
using GameLogic.Arena;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Equipment.Shields;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Armor;
using GameLogic.Modifiers.Character.Block;
using GameLogic.Modifiers.Character.Health;
using GameLogic.Modifiers.Character.MagicDamage;
using GameLogic.Modifiers.Character.Mana;
using GameLogic.Modifiers.Character.PhysicalDamage;
using GameLogic.SkillTree.Paths;
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

        #region Armor
        private int _currentArmor;

        public int BonusArmor { get; protected set; }

        public void AddBonusArmor(int amount)
        {
            BonusArmor += amount;
        }

        public int Armor
        {
            get { return _currentArmor; }
        }

        public int BaseArmor
        {
            get
            {
                return CharacterEquipment == null 
                    ? 0 
                    : CharacterEquipment.Where(i => i is IArmor).Cast<IArmor>().Sum(i => i.ArmorValue);
            }
        }

        public void SetArmor()
        {
            BonusArmor = 0;
            foreach (var m in _modifiers.Where(i => i is ArmorBase))
            {
                m.Apply(this);
            }
            _currentArmor = BonusArmor + BaseArmor;
        }

        public void LoseArmor(int amount)
        {
            _currentArmor = Armor - amount;
        }

        public void GainArmor(int amount)
        {
            _currentArmor = Armor + amount;
        }
        #endregion

        #region Physical Damage

        public int PhysicalDamage { get; protected set; }
        public int PhysicalDamageBonusPercent { get; protected set; }

        public void AddPhysicalDamageBonusPercent(int amount)
        {
            PhysicalDamageBonusPercent += amount;
        }

        public void AddPhysicalDamage(int amount)
        {
            PhysicalDamage += amount;
        }

        public void SetPhysicalDamage()
        {
            PhysicalDamage = 0;
            PhysicalDamageBonusPercent = 0;
            foreach (var m in _modifiers.Where(i => i is PhysicalDamageBase))
            {
                m.Apply(this);
            }
        }

        #endregion

        #region Magic Damage
        public int MagicDamage { get; protected set; }
        public int MagicDamageBonusPercent { get; protected set; }

        public void AddMagicDamageBonusPercent(int amount)
        {
            MagicDamageBonusPercent += amount;
        }

        public void AddMagicDamage(int amount)
        {
            MagicDamage += amount;
        }

        public void SetMagicDamage()
        {
            MagicDamage = 0;
            MagicDamageBonusPercent = 0;
            foreach (var m in _modifiers.Where(i => i is MagicDamageBase))
            {
                m.Apply(this);
            }
        }
        #endregion

        #region Block

        private int _currentBlockAmount;
        public int BonusBlockAmount { get; protected set; }

        public void AddBonusBlockAmount(int amount)
        {
            BonusBlockAmount += amount;
        }

        public int BlockAmount
        {
            get
            {
                return _currentBlockAmount;
            }
        }

        public int BaseBlockAmount
        {
            get
            {
                return CharacterEquipment == null
                                    ? 0
                                    : CharacterEquipment.Where(i => i is Shield).Cast<Shield>().Sum(i => i.BlockValue);
            }
        }

        public void SetBlockAmount()
        {
            BonusBlockAmount = 0;
            foreach (var m in _modifiers.Where(i => i is BlockBase))
            {
                m.Apply(this);
            }
            _currentBlockAmount = BonusBlockAmount + BaseBlockAmount;
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
        private readonly List<IBuyableEquipment> _characterEquipment;

        public List<IAction> CurrentAvailableActions { get; private set; }

        public List<IBuyableEquipment> CharacterEquipment
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

        public void AddAction(IAction action)
        {
            if (!_nativeActions.Exists(i => i.Name == action.Name))
            {
                _nativeActions.Add(action);
            }
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
            //TODO : Check that this still works.
            //if (!_modifiers.Exists(i => i.Name ==  modifier.Name))
            //{
                modifier.Apply(this);
                _modifiers.Add(modifier);
            //}
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
            SkillPoints = 0;
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

        public void AddSkillPoints(int points)
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

        public void ChooseSkill(IPath skill)
        {
            if (skill.Cost > SkillPoints)
            {
                throw new Exception("You cannot afford this skill.");
            }
            _skillTree.TakeSkill(skill, this);
            SkillPoints -= skill.Cost;
            SetAttributes();
        }

        public SkillBranches? CurrentClass
        {
            get { return CurrentPath.Path; }
        }

        public IPath CurrentPath
        {
            get
            {
                var activeSkills = SkillTree.Get().Where(i => i.IsActive).ToList();
                return activeSkills.Any()
                    ? activeSkills.OrderByDescending(i => i.Level).First()
                    : new PathOfTheNothing();
            }
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
        
        #endregion

        protected Character()
        {
            _modifiers = new List<IModifier<ICharacter>>();
            SetAttributes();
            _characterEquipment = new List<IBuyableEquipment>();
            _skillTree = new SkillTree.SkillTree();

            _slots = new List<Slot>();

            var h1 = new Hand();
            _slots.Add(h1);

            var h2 = new Hand();
            _slots.Add(h2);

            _nativeActions = new List<IAction>
            {
                new Run()
            };
        }

        public void SetAttributes()
        {
            SetHealth();
            SetMana();
            SetArmor();
            SetPhysicalDamage();
            SetBlockAmount();
        }
    }
}
