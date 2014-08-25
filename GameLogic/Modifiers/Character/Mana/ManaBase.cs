using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Mana
{
    public abstract class ManaBase : ModifiersBase<ICharacter>, IModifier<ICharacter>
    {
        protected int Percentage;
        protected int Amount;

        public override void Apply(ICharacter c)
        {
            c.AddBonusMana(Percentage > 0 ? (Percentage * c.BaseMana) / 100 : Amount);
        }
    }
}
