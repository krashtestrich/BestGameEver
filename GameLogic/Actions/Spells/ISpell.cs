namespace GameLogic.Actions.Spells
{
    public interface ISpell
    {
        int ManaCost { get; }
        int HitsForFrom { get; }
        int HitsForTo { get; }
    }
}
