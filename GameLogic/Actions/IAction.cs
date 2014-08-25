namespace GameLogic.Actions
{
    public interface IAction
    {
        bool CanBePerformed(IGameEntity source);
        string Name { get; }
        Equipment.Equipment PerformedWith { get; }
        void Perform(IGameEntity source);
    }
}
