namespace GameLogic.Actions
{
    public interface IAction
    {
        bool CanBePerformed(IGameEntity source);
        string Name { get; }
        Equipment.EquipmentBase PerformedWith { get; }
        void Perform(IGameEntity source);
    }
}
