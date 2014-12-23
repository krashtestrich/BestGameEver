using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Armor
{
    public class ArmorBonusAmount : ArmorBase, IModifier<ICharacter>
    {
        public ArmorBonusAmount(int amount)
        {
             Amount = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Amount.ToString(CultureInfo.InvariantCulture)
                   + " armor";
        }
    }
}
