using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Helpers;

namespace GameLogic.Actions.Spells.Damage
{
    public abstract class DamageBase : SpellBase
    {
        public virtual void Perform(IGameEntity source)
        {
            var damage = new ThreadSafeRandom().Next(HitsForFrom, HitsForTo);
            damage = DamageBlockHelper.GetSpellDamage((Character)source, damage);
            ((Character)source.TargettedTile.GetTileEntity()).TakeSpellDamage(damage);
            ((Character)source).LoseMana(ManaCost);
        }

        public virtual bool CanBePerformed(IGameEntity source)
        {
            if (!(source.TargettedTile.GetTileEntity() is ICharacter))
                return false;
            if (((Character)source).GetAlliance() == ((Character)source.TargettedTile.GetTileEntity()).GetAlliance())
                return false;
            if (((Character)source).Mana < ManaCost)
                return false;

            var distance = ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation.GetTileLocation(), source.TargettedTile.GetTileLocation());
            return InRange(distance);
        }
    }
}
