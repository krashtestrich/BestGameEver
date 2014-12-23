using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.PhysicalDamage
{
    public class PhysicalDamagePercentage : PhysicalDamageBase, IModifier<ICharacter>
    {
        public PhysicalDamagePercentage(int amount)
        {
            Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Percentage.ToString(CultureInfo.InvariantCulture)
                   + "% physical damage";
        }
    }
}
