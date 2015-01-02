using System;
using GameLogic.Arena;
using GameLogic.Characters;

namespace GameLogic.Actions.Spells.Heals
{
    public abstract class HealBase : SpellBase
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
                throw  new Exception("Target cannot be null....");
            }
            var healAmount = Helpers.SecureRandom.Next(HitsForFrom, HitsForTo);
            //TODO : Make more sophisticated.
            targetCharacter.ReceiveHeal(healAmount);
            sourceCharacter.LoseMana(ManaCost);
            return sourceCharacter.Name + " " + Verb + " " + Name +  " on " + targetCharacter.Name + " for " + healAmount;
        }

        public virtual bool CanBePerformed(IGameEntity source)
        {
            if (!(source.TargettedTile.GetTileEntity() is ICharacter))
                return false;
            if (((Character)source).GetAlliance() != ((Character)source.TargettedTile.GetTileEntity()).GetAlliance())
                return false;
            if (((Character) source).Mana < ManaCost)
                return false;
            

            var distance = ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation.GetTileLocation(), source.TargettedTile.GetTileLocation());
            return InRange(distance);
        }
    }
}
