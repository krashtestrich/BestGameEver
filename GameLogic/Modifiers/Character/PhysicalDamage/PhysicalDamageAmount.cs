using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.PhysicalDamage
{
    public class PhysicalDamageAmount : PhysicalDamageBase, IModifier<ICharacter>
    {
        public PhysicalDamageAmount(int amount)
        {
            Amount = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Amount.ToString(CultureInfo.InvariantCulture)
                   + " physical damage";
        }
    }
}
