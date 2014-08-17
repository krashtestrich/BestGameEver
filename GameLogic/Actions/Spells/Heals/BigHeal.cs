﻿namespace GameLogic.Actions.Spells.Heals
{
    public class BigHeal : HealBase, IAction
    {
        public override string Name
        {
            get { return "Big Heal"; }
        }

        public override int HitsForFrom
        {
            get { return 15; }
        }

        public override int HitsForTo
        {
            get { return 30; }
        }

        public override int MinRange
        {
            get { return 0; }
        }

        public override int MaxRange
        {
            get { return 0; }
        }

        public override int ManaCost
        {
            get { return 30; }
        }
    }
}