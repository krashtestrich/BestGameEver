using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Enums;
using GameLogic.Slots;

namespace GameLogic.Equipment.Weapons
{
    public sealed class Sword : Weapon, IBuyableEquipment
    {
        public override string Name
        {
            get
            {
                return "Sword";
            }
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

        public override EquipmentType EquipmentType
        {
            get { return EquipmentType.OneHandedWeapon; }
        }

        public override int BaseDamage
        {
            get { return 10; }
        }

        public override int BonusDamage
        {
            get { return 5; }
        }

        public override int Price
        {
            get { return 60;  }
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
        

        public Sword()
        {
            AddSlotType(new Hand());
        }
    }
}
