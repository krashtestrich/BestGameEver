using System.Collections.Generic;
using GameLogic.Enums;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class BestShieldEver : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Best Shield Ever"; }
        }

        public override List<EquipmentSubType> EquipmentSubTypes
        {
            get 
            { 
                return new List<EquipmentSubType>
                {
                    EquipmentSubType.DefensiveFighter
                }; 
            }
        }

        public override int Price
        {
            get { return 9999999; }
        }

        public override int ArmorValue
        {
            get { return 1000; }
        }

        public override int BlockValue
        {
            get { return 100; }
        }

        public BestShieldEver()
        {
            AddSlotType(new Hand());
        }
    }
}
