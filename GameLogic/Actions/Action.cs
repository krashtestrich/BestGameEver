namespace GameLogic.Actions
{
    public abstract class Action
    {
        public abstract string Name
        {
            get;
        }

        public Equipment.EquipmentBase PerformedWith { get; set; }
    }
}
