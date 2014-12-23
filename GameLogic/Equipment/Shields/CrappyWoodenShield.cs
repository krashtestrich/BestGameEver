using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class CrappyWoodenShield : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Crappy Wooden shield"; }
        }

        public override List<EquipmentSubType> EquipmentSubTypes
        {
            get
            {
                return new List<EquipmentSubType>
                {
                    EquipmentSubType.Caster,
                    EquipmentSubType.DefensiveFighter
                };
            }
        }

        public override int Price
        {
            get { return 40; }
        }

        public override int ArmorValue
        {
            get { return 250; }
        }

        public override int BlockValue
        {
            get { return 10; }
        }

        public override List<IModifier<ICharacter>> Modifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new HealthBonusAmount(25)
                };
            }
        }

        public CrappyWoodenShield()
        {
            AddSlotType(new Hand());
        }
    }
}
