using GameLogic.Arena;

namespace GameLogic
{
    public interface IGameEntity
    {
        ArenaFloorPosition ArenaLocation { get; }
    }
}
