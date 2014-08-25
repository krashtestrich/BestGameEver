namespace GameLogic.Modifiers.Character.Health
{
    public sealed class HealthBonusPercentage : HealthBase
    {
        public HealthBonusPercentage(int amount)
        {
            Percentage = amount;
        }
    }
}
