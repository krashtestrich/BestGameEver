using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Armor;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class BrokenWoodenDoor : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Broken wooden door"; }
        }

        public override List<EquipmentSubType> EquipmentSubTypes
        {
            get
            {
                return new List<EquipmentSubType>
                {
                    EquipmentSubType.DefensiveFighter,
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
                    new ArmorBonusPercentage(25)
                };
            }
        }

        public BrokenWoodenDoor()
        {
            AddSlotType(new Hand());
        }
    }
}
