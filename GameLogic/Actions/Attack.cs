using GameLogic.Arena;
using GameLogic.Characters;

namespace GameLogic.Actions
{
    public abstract class Attack : Action
    {
        public abstract int BaseDamageModifier
        {
            get;           
        }

        public abstract int BonusDamageModifier
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

        public virtual bool CanBePerformed(IGameEntity c, ArenaFloorTile f)
        {
            if (f.GetTileEntity().GetType() != typeof(Character) && c.GetType() != typeof(Character))
                return false;
            var distance = ArenaHelper.GetDistanceBetweenFloorPositions(c.ArenaLocation, f.GetTileLocation());
            return distance <= MaxRange
                && distance >= MinRange;
        }
    } 
}