using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Health
{
    public abstract class HealthBase : ModifiersBase<ICharacter>, IModifier<ICharacter>
    {
        public abstract string Name { get; }

        public virtual int Percentage
        {
            get { return 0; }
        }
        public virtual int Amount {
            get { return 0; }
        }

        public override void Apply(ICharacter c)
        {
            c.AddBonusHealth(Percentage > 0 ? (Percentage * c.BaseHealth) / 100 : Amount);
        }
    }
}
