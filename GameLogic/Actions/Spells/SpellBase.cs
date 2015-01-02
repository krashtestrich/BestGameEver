namespace GameLogic.Actions.Spells
{
    public abstract class SpellBase : Action
    {
        protected override string Verb
        {
            get { return "casts"; }
        }

        public abstract int HitsForFrom
        {
            get;
        }

        public abstract int HitsForTo
        {
            get;
        }

        public abstract int MinRange
        {
            get;
        }

        public abstract int MaxRange
        {
            get;
        }

        public abstract int ManaCost
        {
            get;
        }

        protected bool InRange(int distance)
        {
            return distance <= MaxRange
                && distance >= MinRange;
        }
    } 
}
