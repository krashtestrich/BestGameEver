namespace GameLogic.Modifiers
{
    public interface IModifier<in TType>
    {
        void Apply(TType modType);
        string Name { get; }
    }
}
