using System.Collections.Generic;
using GameLogic.Actions;

namespace GameLogic.Equipment
{
    public abstract class Shield : Equipment
    {
        public override string EquipmentType
        {
            get { return "Shield"; }
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
        
        #endregion

    }
}
