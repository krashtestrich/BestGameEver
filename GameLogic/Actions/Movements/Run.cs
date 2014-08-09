namespace GameLogic.Actions.Movements
{
    public class Run : Move, IAction
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
