namespace GameLogic.Actions
{
    public interface IAction
    {
        bool CanBePerformed(IGameEntity source);
        string Name { get; }
        Equipment.EquipmentBase PerformedWith { get; }
        string Perform(IGameEntity source);
        
    }
}
