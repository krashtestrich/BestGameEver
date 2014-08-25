namespace GameLogic.Actions
{
    public abstract class Action
    {
        public abstract string Name
        {
            get;
        }

        public Equipment.Equipment PerformedWith { get; set; }
    }
}
