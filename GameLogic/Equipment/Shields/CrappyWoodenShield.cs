using GameLogic.Slots;

namespace GameLogic.Equipment.Shields
{
    public class CrappyWoodenShield : Shield, IBuyableEquipment, IArmor
    {
        public override string Name
        {
            get { return "Crappy Wooden shield"; }
        }

        public override int Price
        {
            get { return 40; }
        }

        public override int ArmorValue
        {
            get { return 75; }
        }

        public CrappyWoodenShield()
        {
            AddSlotType(new Hand());
        }
    }
}
