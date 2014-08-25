﻿namespace GameLogic.Actions.Movements
{
    public class Run : MoveBase, IAction, IMove
    {
        public override string Name
        {
            get { return "Run"; }
        }

        public override int Distance
        {
            get { return 1; }
        }
    }
}
