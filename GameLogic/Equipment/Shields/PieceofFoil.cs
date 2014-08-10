using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class PieceofFoil : Shield
    {
         public override string Name
        {
            get { return "Piece of Foil"; }
        }

        public override int Price
        {
            get { return 15; }
        }

        public override int BaseBlock
        {
            get { return 0; }
        }

        public override int BonusBlock
        {
            get { return 10; }
        }

        public PieceofFoil()
        {
            AddSlotType(new Hand());
        }
    }
}
