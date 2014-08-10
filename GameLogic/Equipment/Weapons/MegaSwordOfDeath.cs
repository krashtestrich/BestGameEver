using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Slots;

namespace GameLogic.Equipment.Weapons
{
    public class MegaSwordOfDeath : Weapon
    {
        public override string Name
        {
            get { return "Mega Sword of Death"; }
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
