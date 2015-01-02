using System;
using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Helpers;

namespace GameLogic.Actions.Spells.Damage
{
    public abstract class DamageBase : SpellBase
    {
        public virtual string Perform(IGameEntity source)
        {
            var sourceCharacter = source as Character;
            var targetCharacter = source.TargettedTile.GetTileEntity() as Character;
            if (sourceCharacter == null)
            {
                throw new Exception("Source cannot be null....");
            }
            if (targetCharacter == null)
            {
                throw new Exception("Target cannot be null....");
            }
            var damage = SecureRandom.Next(HitsForFrom, HitsForTo);
            damage = DamageBlockHelper.GetSpellDamage(sourceCharacter, damage);
            targetCharacter.TakeSpellDamage(damage);
            sourceCharacter.LoseMana(ManaCost);
            return sourceCharacter.Name + " " + Verb + " " + Name + " on " + targetCharacter.Name + " for " + damage;
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
