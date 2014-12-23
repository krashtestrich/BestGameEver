using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Health
{
    public sealed class HealthBonusPercentage : HealthBase, IModifier<ICharacter>
    {
        public HealthBonusPercentage(int amount)
        {
            Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Percentage.ToString(CultureInfo.InvariantCulture)
                   + "% health";
        }
    }
}
