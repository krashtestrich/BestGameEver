namespace GameLogic.Modifiers
{
    public abstract class ModifiersBase<TType>
    {
        protected TType ModType;

        public abstract void Apply(TType modType);
    }
}
