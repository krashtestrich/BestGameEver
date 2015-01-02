namespace GameLogic.Actions.Movements
{
    public class Run : MoveBase, IAction, IMove
    {
        protected override string Verb
        {
            get { return "runs"; }
        }

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
