using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Block
{
    public abstract class BlockBase : ModifiersBase<ICharacter>
    {
        protected int Percentage;

        public override void Apply(ICharacter c)
        {
            c.AddBonusBlockAmount(Percentage);
        }
    }
}
