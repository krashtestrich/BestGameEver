using GameLogic.Characters;

namespace GameLogic.Modifiers.Character.Armor
{
    public abstract class ArmorBase : ModifiersBase<ICharacter>, IModifier<ICharacter>
    {
        protected int Percentage;
        protected int Amount;

        public override void Apply(ICharacter c)
        {
            c.AddBonusArmor(Percentage > 0 ? (Percentage * c.BaseArmor) / 100 : Amount);
        }
    }
}
