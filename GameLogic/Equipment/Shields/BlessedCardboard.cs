using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Health;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class BlessedCardboard : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Blessed Cardboard"; }
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

        public override List<IModifier<ICharacter>> Modifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new HealthBonusAmount(25),
                    new HealthBonusPercentage(10)
                };
            }
        }

        public override int Price
        {
            get { return 15; }
        }

        public override int ArmorValue
        {
            get { return 5; }
        }

        public override int BlockValue
        {
            get { return 5; }
        }

        public BlessedCardboard()
        {
            AddSlotType(new Hand());
        }
    }
}
