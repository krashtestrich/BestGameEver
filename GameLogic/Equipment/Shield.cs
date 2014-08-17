using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Helpers;

namespace GameLogic.Equipment
{
    public abstract class Shield : Equipment
    {
        public override string EquipmentType
        {
            get { return "Shield"; }
        }

        public virtual int GetBlock()
        {
            return BaseBlock + new ThreadSafeRandom().Next(0, BonusBlock);
        }

        public override List<IAction> Actions
        {
            get { return new List<IAction>(); }
        }

        #region Abstract Properties
        public abstract int BaseBlock
        {
            get;
        }

        public abstract int BonusBlock
        {
            get;
        }
        #endregion

    }
}
