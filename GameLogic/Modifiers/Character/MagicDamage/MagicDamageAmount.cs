using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.MagicDamage
{
    public class MagicDamageAmount : MagicDamageBase, IModifier<ICharacter>
    {
        public MagicDamageAmount(int amount)
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
