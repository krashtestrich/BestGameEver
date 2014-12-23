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

        public static void TakePhysicalDamage(ICharacter character, int damage)
        {
            if (character.BlockAmount > 0)
            {
                var r = new Helpers.ThreadSafeRandom();
                if (r.Next(1, 100) <= character.BlockAmount)
                {
                    return;
                }
            }

            var damageReduction = (Convert.ToDecimal(character.Armor)) / Convert.ToDecimal(character.Armor + (12 * damage));
            var damageToTake = damage - damage * damageReduction;
            character.LoseHealth(Convert.ToInt32(Math.Ceiling(damageToTake)));
        }

        public static int GetSpellDamage(ICharacter character, int damage)
        {
            damage += character.MagicDamage;
            return damage + Convert.ToInt32(Math.Ceiling(damage * Convert.ToDecimal(character.MagicDamageBonusPercent) / 100));
        }
    }
}
