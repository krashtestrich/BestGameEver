using GameLogic.Arena;

namespace GameLogic.Actions.Movements
{
    public abstract class Move : Action
    {
        public abstract int Distance
        { get; }

        public virtual bool CanBePerformed(IGameEntity source)
        {
            return source.TargettedTile.GetTileEntity() == null 
                && ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation.GetTileLocation(), source.TargettedTile.GetTileLocation()) > Distance;
        }

        public virtual void PerformAction(IGameEntity source)
        {
            source.ArenaLocation.RemoveEntityFromTile(source);
            source.TargettedTile.AddEntityToTile(source);
        }
    }
}
