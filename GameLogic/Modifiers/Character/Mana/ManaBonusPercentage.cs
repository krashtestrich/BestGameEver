using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Mana
{
    public class ManaBonusPercentage : ManaBase, IModifier<ICharacter>
    {
        public ManaBonusPercentage(int amount)
        {
            Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Percentage.ToString(CultureInfo.InvariantCulture)
                   + "% mana";
        }
    }
}
