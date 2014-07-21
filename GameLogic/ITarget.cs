using GameLogic.Arena;

namespace GameLogic
{
    public interface IGameEntity
    {
        ArenaFloorTile ArenaLocation { get; }
        void SetEntityLocation(ArenaFloorTile tile);
        ArenaFloorTile TargettedTile { get; }
    }
}
