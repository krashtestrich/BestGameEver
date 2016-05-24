using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Enums;
using GameLogic.Slots;

namespace GameLogic.Equipment.Weapons
{
    public sealed class BigSword : Weapon, IBuyableEquipment
    {     
        public override string Name
        {
            get
            {
                return "Big Sword";
            }
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
            get { return 25; }
        }

        public override int BonusDamage
        {
            get { return 20; }
        }

        public override int Price
        {
            get { return 75; }
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

        public BigSword()
        {
            AddSlotType(new Hand());
            AddSlotType(new Hand());
        }
    }
}
