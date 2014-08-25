using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Slots;

namespace GameLogic.Equipment.Weapons
{
    public class ReliableTreeBranch : Weapon, IBuyableEquipment
    {
        public override string Name
        {
            get
            {
                return "Reliable Tree Branch";
            }
        }

        public override int BaseDamage
        {
            get { return 30; }
        }

        public override int BonusDamage
        {
            get { return 0; }
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

        public ReliableTreeBranch()
        {
            AddSlotType(new Hand());
            AddSlotType(new Hand());
        }
    }
}
