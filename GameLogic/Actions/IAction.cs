using GameLogic.Arena;

namespace GameLogic.Actions
{
    //public interface IAction<in TSource, in TTarget : ITarget>
    //{
    //    bool CanBePerformed(TSource s, TTarget t);
    //    ITarget Target { get; }
    //    Equipment.Equipment PerformedWith { get; }
    //}

    public interface IAction
    {
        bool CanBePerformed(IGameEntity source, ArenaFloorTile targetTile);
        string Name { get; }
        Equipment.Equipment PerformedWith { get; }
    }
}
