namespace GameLogic.Actions
{
    public abstract class Action
    {
        public abstract string Name
        {
            get;
        }

        protected abstract string Verb
        {
            get;
        }

        public Equipment.EquipmentBase PerformedWith { get; set; }
    }
}
