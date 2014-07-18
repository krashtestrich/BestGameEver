namespace GameLogic
{
    public abstract class Slot
    {
        #region Slot Free

        public bool SlotFree { get; private set; }

        public void SetSlotFree(bool free, string equipName)
        {
            SlotFree = free;
            SlotEquipmentName = equipName;
        }

        public string SlotEquipmentName { get; private set; }

        #endregion

        #region Slot Type

        public abstract string SlotType
        {
            get;
        }
        #endregion

        public Slot()
        {
            SlotFree = true;
        }
    }
}
