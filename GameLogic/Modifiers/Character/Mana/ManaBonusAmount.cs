using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Mana
{
    public class ManaBonusAmount : ManaBase, IModifier<ICharacter>
    {
        public ManaBonusAmount(int amount)
        {
            Amount = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Amount.ToString(CultureInfo.InvariantCulture)
                   + " mana";
        }
    }
}
