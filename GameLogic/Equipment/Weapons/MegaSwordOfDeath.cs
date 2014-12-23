using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Enums;
using GameLogic.Slots;

namespace GameLogic.Equipment.Weapons
{
    public class MegaSwordOfDeath : Weapon, IBuyableEquipment
    {
        public override string Name
        {
            get { return "Mega Sword of Death"; }
        }

        public override List<EquipmentSubType> EquipmentSubTypes
        {
            get
            {
                return new List<EquipmentSubType>
                {
                    EquipmentSubType.OffensiveFighter
                };
            }
        }

        public override EquipmentType EquipmentType
        {
            get { return EquipmentType.TwoHandedWeapon; }
        }

        public override int BaseDamage
        {
            get { return 100; }
        }

        public override int BonusDamage
        {
            get { return 55; }
        }

        public override int Price
        {
            get { return 1000000000;  }
        }

        public override List<IAction> Actions
        {
            get
            {
                return new List<IAction>
                {
                        new Swing
                        {
                            PerformedWith = this
                        }
                    };
            }
        }


        public MegaSwordOfDeath()
        {
            AddSlotType(new Hand());
        }
    }
}
