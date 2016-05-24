using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.MagicDamage;
using GameLogic.Modifiers.Character.Mana;
using GameLogic.Slots;

namespace GameLogic.Equipment.Weapons
{
    public class GnarledStaff: Weapon, IBuyableEquipment
    {
        public override string Name
        {
            get { return "Gnarled Staff"; }
        }

        public override List<EquipmentSubType> EquipmentSubTypes
        {
            get
            {
                return new List<EquipmentSubType>
                {
                    EquipmentSubType.Caster
                };
            }
        }

        public override EquipmentType EquipmentType
        {
            get { return EquipmentType.TwoHandedWeapon; }
        }

        public override int BaseDamage
        {
            get { return 20; }
        }

        public override int BonusDamage
        {
            get { return 10; }
        }

        public override int Price
        {
            get { return 100; }
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

        public override List<IModifier<ICharacter>> Modifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new ManaBonusPercentage(20),
                    new MagicDamageAmount(15)
                };
            }
        }


        public GnarledStaff()
        {
            AddSlotType(new Hand());
        }
    }
}
