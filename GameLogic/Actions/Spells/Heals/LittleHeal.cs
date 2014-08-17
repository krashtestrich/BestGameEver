namespace GameLogic.Actions.Spells.Heals
{
    public class LittleHeal : HealBase, IAction
    {
        public override string Name
        {
            get { return "Little heal"; }
        }

        public override int HitsForFrom
        {
            get { return 5; }
        }

        public override int HitsForTo
        {
            get { return 15; }
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
            get { return 15; }
        }
    }
}
