using System.Collections.Generic;
using GameLogic.Enums;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class PieceofFoil : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Piece of Foil"; }
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
            get { return 15; }
        }

        public override int ArmorValue
        {
            get { return 50; }
        }

        public override int BlockValue
        {
            get { return 5; }
        }

        public PieceofFoil()
        {
            AddSlotType(new Hand());
        }
    }
}
