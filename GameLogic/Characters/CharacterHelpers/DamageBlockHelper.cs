using System;

namespace GameLogic.Characters.CharacterHelpers
{
    public class DamageBlockHelper
    {
        public static int GetPhysicalDamage(ICharacter character, int damage)
        {
            damage += character.PhysicalDamage;
            return damage + Convert.ToInt32(Math.Ceiling(damage * Convert.ToDecimal(character.PhysicalDamageBonusPercent) / 100));
        }

        public static int TakePhysicalDamage(ICharacter character, int damage)
        {
            if (character.BlockAmount > 0)
            {
                //if (Helpers.SecureRandom.Next(1, 100) <= character.BlockAmount)
               // {
                //    return 0;
                //}
            }
            var minimumDamage = Math.Ceiling(Convert.ToDecimal(damage)*33/100);
            var damageReduction = (Convert.ToDecimal(character.Armor)) / Convert.ToDecimal(character.Armor + (12 * damage));
            var damageToTake = Convert.ToInt32(Math.Ceiling(minimumDamage + (damage - minimumDamage - (damage * damageReduction))));
            character.LoseHealth(damageToTake);
            return damageToTake;
        }

        public static int GetSpellDamage(ICharacter character, int damage)
        {
            damage += character.MagicDamage;
            return damage + Convert.ToInt32(Math.Ceiling(damage * Convert.ToDecimal(character.MagicDamageBonusPercent) / 100));
        }
    }
}
