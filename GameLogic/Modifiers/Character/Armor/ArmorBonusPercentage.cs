using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Armor
{
    public class ArmorBonusPercentage : ArmorBase, IModifier<ICharacter>
    {
        public ArmorBonusPercentage(int amount)
        {
             Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Amount.ToString(CultureInfo.InvariantCulture)
                   + "% armor";
        }
    }
}
