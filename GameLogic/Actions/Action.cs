using System;

namespace GameLogic.Actions
{
    public abstract class Action
    {
        protected Random R;
        public abstract string Name
        {
            get;
        }

        public Equipment.Equipment PerformedWith { get; set; }

        protected Action()
        {
            R = new Random();
        }
    }
}
