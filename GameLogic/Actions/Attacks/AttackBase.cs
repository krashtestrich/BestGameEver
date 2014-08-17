using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Equipment;
using GameLogic.Helpers;

namespace GameLogic.Actions.Attacks
{
    public abstract class AttackBase : Action
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
            var modifier = new ThreadSafeRandom().Next(DamageFromModifier, DamageToModifier);
            var damage = modifier > 0 ? weaponDamage*modifier : weaponDamage;
            //TODO : Make more sophisticated.
            ((Character)source.TargettedTile.GetTileEntity()).TakeDamage(damage);
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