using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Health
{
    public sealed class HealthBonusAmount : HealthBase, IModifier<ICharacter>
    {
        public HealthBonusAmount(int amount)
        {
             Amount = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Amount.ToString(CultureInfo.InvariantCulture)
                   + " health";
        }
    }
}
