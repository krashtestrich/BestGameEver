using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.HealthRegeneration
{
    public class HealthRegenerationAmount : HealthRegenerationBase, IModifier<ICharacter>
    {
        public HealthRegenerationAmount(int amount)
        {
            Amount = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Amount.ToString(CultureInfo.InvariantCulture)
                   + " health regeneration";
        }
    }
}
