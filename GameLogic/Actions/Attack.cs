using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Equipment;

namespace GameLogic.Actions
{
    public abstract class Attack : Action
    {
        public abstract int DamageFromModifier
        {
            get;           
        }

        public abstract int DamageToModifier
        {
            get;            
        }

        public abstract int MinRange
        {
            get;                             
        }

        public abstract int MaxRange
        {
            get;
        }

        private bool InRange(int distance)
        {
            return distance <= MaxRange
                && distance >= MinRange;
        }

        public virtual void PerformAction(IGameEntity source)
        {
            var weaponDamage = ((Weapon) PerformedWith).GetDamage();
            var modifier = R.Next(DamageFromModifier, DamageToModifier);
            var damage = modifier > 0 ? weaponDamage*modifier : weaponDamage;
            ((Character)source.TargettedTile.GetTileEntity()).LoseHealth(damage);
        }

        public virtual bool CanBePerformed(IGameEntity source)
        {
            if (!(source.TargettedTile.GetTileEntity() is ICharacter))
                return false;
            if (((Character)source).GetAlliance() == ((Character)source.TargettedTile.GetTileEntity()).GetAlliance())
                return false;

            var distance = ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation.GetTileLocation(), source.TargettedTile.GetTileLocation());
            return InRange(distance);
        }
    } 
}