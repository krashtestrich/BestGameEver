namespace GameLogic.Equipment
{
    public abstract class Weapon : Equipment
    {
        public override string EquipmentType
        {
            get {                             
                return "Weapon"; 
            }
        }

        public virtual int GetDamage()
        {
            return BaseDamage + R.Next(0, BonusDamage);
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
