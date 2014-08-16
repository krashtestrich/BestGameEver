namespace GameLogic.Modifiers.Character.Health
{
    public class SuperHealth : HealthBase
    {
        public override string Name
        {
            get { return "Super Health"; }
        }

        public override int Percentage
        {
            get { return 20; }
        }
    }
}
