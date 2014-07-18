using GameLogic.Arena;

namespace GameLogic.Actions.Movements
{
    public abstract class Move : Action
    {
        public abstract int Distance
        { get; }

        public virtual bool CanBePerformed(IGameEntity source, ArenaFloorTile tile)
        {
            return ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation, tile.GetTileLocation()) <= Distance;
        }
    }
}
