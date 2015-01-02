using System;
using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Helpers;

namespace GameLogic.Actions.Attacks
{
    public abstract class AttackBase : Action
    {
        protected override string Verb
        {
            get { return "attacks"; }
        }

        public abstract int DamageFromModifier
        {
            get;           
        }

        public abstract int DamageToModifier
        {
            get;            
        }

        public abstract int MinRange
        {
            get;                             
        }

        public abstract int MaxRange
        {
            get;
        }

        private bool InRange(int distance)
        {
            return distance <= MaxRange
                && distance >= MinRange;
        }

        public virtual string Perform(IGameEntity source)
        {
            var character = source as Character;
            if (character == null)
            {
                throw new Exception("Source cannot be null....");
            }
            var target = source.TargettedTile.GetTileEntity() as Character;
            if (target == null)
            {
                throw new Exception("Target cannot be null....");
                
            }
            var weaponDamage = ((Equipment.Weapons.Weapon) PerformedWith).GetDamage();
            var modifier = SecureRandom.Next(DamageFromModifier, DamageToModifier);
            var damage = modifier > 0 ? weaponDamage*(modifier/100) : weaponDamage;
            damage = DamageBlockHelper.GetPhysicalDamage(character, damage);
            var takenDamage = DamageBlockHelper.TakePhysicalDamage(target, damage);
            return character.Name + " " + Verb + " " + target.Name + " with " + Name + " for " + damage + ". "
                + target.Name + " blocks " + (damage - takenDamage) + " loses " + takenDamage + " health.";
        }

        public virtual bool CanBePerformed(IGameEntity source)
        {
            if (!(source.TargettedTile.GetTileEntity() is ICharacter))
                return false;
            if (((Character)source).GetAlliance() == ((Character)source.TargettedTile.GetTileEntity()).GetAlliance())
                return false;

            var distance = ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation.GetTileLocation(), source.TargettedTile.GetTileLocation());
            return InRange(distance);
        }
    } 
}