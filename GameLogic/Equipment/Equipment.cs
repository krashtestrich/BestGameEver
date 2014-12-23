using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;

namespace GameLogic.Equipment
{
    public abstract class EquipmentBase
    {

        #region Name

        public abstract string Name
        {
            get;
        }
    
        #endregion

        #region Equipment Type

        public abstract EquipmentType EquipmentType { get; }

        public abstract List<EquipmentSubType> EquipmentSubTypes { get; }

        #endregion

        #region Slots
        private readonly List<Slot> _slots;

        public void AddSlotType(Slot slot)
        {
            _slots.Add(slot);
        } 

        public List<Slot> Slots
        {
            get
            {
                return _slots;
            }
        }
        #endregion

        #region Price
        public abstract int Price
        {
            get;
        }

        #endregion

        #region Actions
        public abstract List<IAction> Actions
        {
            get;
        }
        #endregion

        #region Modifiers

        public virtual List<IModifier<ICharacter>> Modifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>();
            }
        }

        #endregion

        protected EquipmentBase()
        {
            _slots = new List<Slot>();
        }
    }
}
