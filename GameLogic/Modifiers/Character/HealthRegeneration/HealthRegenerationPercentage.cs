using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.HealthRegeneration
{
    public class HealthRegenerationPercentage : HealthRegenerationBase, IModifier<ICharacter>
    {
        public HealthRegenerationPercentage(int amount)
        {
            Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Percentage.ToString(CultureInfo.InvariantCulture)
                   + "% health generation";
        }
    }
}
