using System.Globalization;
using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Block
{
    public class BlockPercentage : BlockBase, IModifier<ICharacter>
    {
        public BlockPercentage(int amount)
        {
            Percentage = amount;
        }

        public override string GetDisplayText()
        {
            return "+" + Percentage.ToString(CultureInfo.InvariantCulture)
                   + "% block";
        }
    }
}
