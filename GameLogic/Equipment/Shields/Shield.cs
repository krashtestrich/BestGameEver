using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Enums;

namespace GameLogic.Equipment.Shields
{
    public abstract class Shield : EquipmentBase
    {
        public override EquipmentType EquipmentType
        {
            get { return EquipmentType.Shield; }
        }

        public override List<IAction> Actions
        {
            get { return new List<IAction>(); }
        }

        #region Abstract Properties
        public abstract int ArmorValue
        {
            get;
        }

        public abstract int BlockValue { get; }
        
        #endregion

    }
}
