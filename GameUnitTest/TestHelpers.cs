using System.Collections.Generic;
using GameLogic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Equipment.Weapons;

namespace GameUnitTest
{
    public class TestHelpers
    {
        #region Test Objects
        #region Attacks

        public class TestAttack : AttackBase, IAction
        {
            public override string Name
            {
                get { return "Test Attack"; }
            }

            public override int DamageFromModifier
            {
                get { return 1; }
            }

            public override int DamageToModifier
            {
                get { return 0; }
            }

            public override int MaxRange
            {
                get { return 1; }
            }

            public override int MinRange
            {
                get { return 1; }
            }
        }

        public class TestAttack2 : AttackBase, IAction
        {
            public bool CanBePerformed(ICharacter source, IGameEntity gameEntity)
            {
                throw new System.NotImplementedException();
            }

            public override string Name
            {
                get { return "Test Attack2"; }
            }

            public override int DamageFromModifier
            {
                get { return 0; }
            }

            public override int DamageToModifier
            {
                get { return 0; }
            }

            public override int MaxRange
            {
                get { return 1; }
            }

            public override int MinRange
            {
                get { return 1; }
            }
        }

        #endregion

        #region Weapons
        public class TestWeapon : Weapon, IBuyableEquipment
        {

            public override string Name
            {
                get { return "Test Weapon"; }
            }

            public override int BaseDamage
            {
                get { return 5; }
            }

            public override int BonusDamage
            {
                get { return 3; }
            }

            public override EquipmentType EquipmentType
            {
                get
                {
                    return EquipmentType.OneHandedWeapon;
                }
            }

            public override List<EquipmentSubType> EquipmentSubTypes
            {
                get
                {
                    return new List<EquipmentSubType>
                    {
                        EquipmentSubType.Caster,
                        EquipmentSubType.DefensiveFighter,
                        EquipmentSubType.OffensiveFighter
                    };
                }
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
                            new TestAttack()
                        };
                }
            }
        }

        public class TestWeapon2 : Weapon
        {
            public override string Name
            {
                get { return "Test Weapon2"; }
            }

            public override int BaseDamage
            {
                get { return 5; }
            }

            public override int BonusDamage
            {
                get { return 3; }
            }

            public override EquipmentType EquipmentType
            {
                get
                {
                    return EquipmentType.OneHandedWeapon;
                }
            }

            public override List<EquipmentSubType> EquipmentSubTypes
            {
                get
                {
                    return new List<EquipmentSubType>
                    {
                        EquipmentSubType.Caster,
                        EquipmentSubType.DefensiveFighter,
                        EquipmentSubType.OffensiveFighter
                    };
                }
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
                            new TestAttack2()
                        };
                }
            }
        }
        #endregion

        #region Characters
        public class TestCharacter : Character
        {
           public TestCharacter()
           {
                SetName("Test Character");
            }

            public override int BaseHealth
            {
                get { return 100; }
            }

            public override int BaseMana
            {
                get { return 50; }
            }
        }
        #endregion

        #endregion
    }
}
