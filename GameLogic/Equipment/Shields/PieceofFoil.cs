using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class PieceofFoil : Shield, IBuyableEquipment, IArmor
    {
         public override string Name
        {
            get { return "Piece of Foil"; }
        }

        public override int Price
        {
            get { return 15; }
        }

        public override int ArmorValue
        {
            get { return 25; }
        }

        public PieceofFoil()
        {
            AddSlotType(new Hand());
        }
    }
}
