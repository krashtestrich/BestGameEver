namespace GameLogic.Actions.Attacks
{
    public interface IAttack
    {
        int DamageFromModifier { get; }
        int DamageToModifier { get; }
    }
}
