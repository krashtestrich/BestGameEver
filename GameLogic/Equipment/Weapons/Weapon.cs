using GameLogic.Helpers;

namespace GameLogic.Equipment.Weapons
{
    public abstract class Weapon : EquipmentBase
    {
        public virtual int GetDamage()
        {
            return BaseDamage + new ThreadSafeRandom().Next(0, BonusDamage);
        }

        #region Abstract Properties
        public abstract int BaseDamage
        {
            get;
        }

        public abstract int BonusDamage
        {
           get;
        }

        #endregion
    }
}
