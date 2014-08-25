using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Health
{
    public abstract class HealthBase : ModifiersBase<ICharacter>, IModifier<ICharacter>
    {
        protected int Percentage;
        protected int Amount;

        public override void Apply(ICharacter c)
        {
            c.AddBonusHealth(Percentage > 0 ? (Percentage * c.BaseHealth) / 100 : Amount);
        }
    }
}
