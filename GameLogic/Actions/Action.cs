namespace GameLogic.Actions
{
    //public abstract class Action<TSource, TTarget> : IAction<TSource, TTarget>
    //{
    //    protected Random R;
    //    public abstract bool CanBePerformed(TSource s, TTarget t);
    //    public ITarget Target { get; set; }
    //    public Equipment.Equipment PerformedWith { get; set; }

    //    protected Action()
    //    {
    //        R = new Random();
    //    }
    //}

    public abstract class Action
    {
        public abstract string Name
        {
            get;
        }

        public Equipment.Equipment PerformedWith { get; set; }
    }
}
