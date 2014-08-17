using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Mana
{
    public abstract class ManaBase : ModifiersBase<ICharacter>, IModifier<ICharacter>
    {
        public abstract string Name { get; }

        public virtual int Percentage
        {
            get { return 0; }
        }
        public virtual int Amount
        {
            get { return 0; }
        }

        public override void Apply(ICharacter c)
        {
            c.AddBonusMana(Percentage > 0 ? (Percentage * c.BaseMana) / 100 : Amount);
        }
    }
}
