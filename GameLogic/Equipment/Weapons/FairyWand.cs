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
    public sealed class FairyWand : Weapon, IBuyableEquipment
    {
        public override string Name
        {
            get { return "Fairy Wand"; }
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
            get { return EquipmentType.OneHandedWeapon; }
        }

        public override int BaseDamage
        {
            get { return 8; }
        }

        public override int BonusDamage
        {
            get { return 5; }
        }

        public override int Price
        {
            get { return 60; }
        }

        public override List<IAction> Actions
        {
            get
            {
                return new List<IAction>
                {
                    new Shoot
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
                    new ManaBonusPercentage(10),
                    new MagicDamageAmount(10)
                };
            }
        }


        public FairyWand()
        {
            AddSlotType(new Hand());
        }
    }
}
