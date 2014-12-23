using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.PhysicalDamage
{
    public abstract class PhysicalDamageBase : ModifiersBase<ICharacter>
    {
        protected int Percentage;
        protected int Amount;

        public override void Apply(ICharacter c)
        {
            c.AddPhysicalDamage(Amount);
            c.AddPhysicalDamageBonusPercent(Percentage);
        }
    }
}
