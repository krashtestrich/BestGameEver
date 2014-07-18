using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Actions.Movements;
using GameLogic.Arena;
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

        public ArenaFloorPosition ArenaLocation { get; private set; }

        public void SetCharacterLocation(int x, int y)
        {
            ArenaLocation = new ArenaFloorPosition(x, y);
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

        public List<Equipment.Equipment> CharacterEquipment
        {
            get
            {
                return _characterEquipment;
            }
        }

        #endregion    

        #region Target

        private ArenaFloorPosition _targettedFloorPosition;
        #endregion

        #region Actions

        private readonly List<IAction> _nativeActions;

        public void ChooseAction(IAction a)
        {
            
        }

        public void PerformAction(IAction a)
        {
            
        }

        public List<IAction> SelectActionsFromTargetTile(ArenaFloorTile tile)
        {
            return _nativeActions.Concat(CharacterEquipment.SelectMany(i => i.Actions).ToList().DistinctBy(i => i.Name)).ToList().Where(i => i.CanBePerformed(this, tile)).ToList();
        }

        #endregion

        #region Battle

        public List<IAction> ChosenActions { get; set; }

        #endregion

        public Character()
        {
            _characterEquipment = new List<Equipment.Equipment>();

            _slots = new List<Slot>();

            var h1 = new Hand();
            _slots.Add(h1);

            var h2 = new Hand();
            _slots.Add(h2);

            ChosenActions = new List<IAction>();

            _nativeActions = new List<IAction>
            {
                new Run()
            };
        }
    }
}
