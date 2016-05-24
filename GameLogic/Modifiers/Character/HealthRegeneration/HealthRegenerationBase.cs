using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.HealthRegeneration
{
    public abstract class HealthRegenerationBase : ModifiersBase<ICharacter>
    {
        protected int Percentage;
        protected int Amount;

        public override void Apply(ICharacter c)
        {
            c.AddBonusHealthRegeneration(Percentage > 0 ? (Percentage * c.BaseHealthRegeneration) / 100 : Amount);
        }
    }
}
