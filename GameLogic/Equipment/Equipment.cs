using System.Collections.Generic;
using GameLogic.Actions;

namespace GameLogic.Equipment
{
    public abstract class Equipment
    {

        #region Name

        public abstract string Name
        {
            get;
        }
    
        #endregion

        #region Equipment Type

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
        }
    }
}
