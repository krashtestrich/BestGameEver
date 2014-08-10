using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Actions.Movements;
using GameLogic.Arena;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Slots;
using MoreLinq;

namespace GameLogic.Characters
{
    public class Character : ICharacter, IGameEntity
    {
        #region Name

        public void SetName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        #endregion

        #region Health

        public int Health { get; private set; }

        public void SetHealth(int health)
        {
            Health = health;
        }

        public void LoseHealth(int amount)
        {
            Health = Health - amount;
        }

        public void GainHealth(int amount)
        {
            Health = Health + amount;
        }

        #endregion

        #region Cash
        public int Cash { get; protected set; }

        public void SetCash(int amount)
        {
            Cash = amount;
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
        #endregion

        #region Level
        private int _level;

        public int GetLevel()
        {
            return _level;
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void LevelUp()
        {
            _level++;
        }
        #endregion

        public Character(Alliance alliance, int level)
        {
            _alliance = alliance;
            _level = level;
            _characterEquipment = new List<Equipment.Equipment>();

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
    }
}
