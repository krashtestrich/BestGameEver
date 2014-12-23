using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.MagicDamage
{
    public abstract class MagicDamageBase : ModifiersBase<ICharacter>
    {
        protected int Percentage;
        protected int Amount;

        public override void Apply(ICharacter c)
        {
            c.AddMagicDamage(Amount);
            c.AddMagicDamageBonusPercent(Percentage);
        }
    }
}
