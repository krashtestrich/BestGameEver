using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class CrappyWoodenShield : Shield
    {
        public override string Name
        {
            get { return "Crappy Wooden shield"; }
        }

        public override int Price
        {
            get { return 40; }
        }

        public override int BaseBlock
        {
            get { return 10; }
        }

        public override int BonusBlock
        {
            get { return 10; }
        }

        public CrappyWoodenShield()
        {
            AddSlotType(new Hand());
        }
    }
}
