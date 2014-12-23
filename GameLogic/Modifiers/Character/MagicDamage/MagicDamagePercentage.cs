using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.MagicDamage
{
    public class MagicDamagePercentage : MagicDamageBase, IModifier<ICharacter>
    {
        public MagicDamagePercentage(int amount)
        {
            Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Percentage.ToString(CultureInfo.InvariantCulture)
                   + "% magic damage";
        }
    }
}
