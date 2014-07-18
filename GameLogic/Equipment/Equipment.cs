using System;
using System.Collections.Generic;
using GameLogic.Actions;

namespace GameLogic.Equipment
{
    public abstract class Equipment
    {
        internal Random R;

        #region Name

        public abstract string Name
        {
            get;
        }
    
        #endregion

        #region Equipment Type
        private string equipmentType;

        public abstract string EquipmentType
        {
            get;
        }

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
        private int price;
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

        protected Equipment()
        {
            _slots = new List<Slot>();
            R = new Random();
        }
    }
}
