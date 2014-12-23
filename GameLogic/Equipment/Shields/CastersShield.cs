using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.MagicDamage;
using GameLogic.Modifiers.Character.Mana;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class CastersShield : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Casters Shield"; }
        }

        public override List<EquipmentSubType> EquipmentSubTypes
        {
            get
            {
                return new List<EquipmentSubType>
                {
                    EquipmentSubType.Caster
                };
            }
        }

        public override int Price
        {
            get { return 40; }
        }

        public override int ArmorValue
        {
            get { return 100; }
        }

        public override int BlockValue
        {
            get { return 20; }
        }

        public override List<IModifier<ICharacter>> Modifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new ManaBonusAmount(25),
                    new MagicDamagePercentage(25)
                };
            }
        }

        public CastersShield()
        {
            AddSlotType(new Hand());
        }
    }
}
