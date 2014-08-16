namespace GameLogic.Modifiers.Character.Health
{
    public class WeeBitOfHealth : HealthBase
    {
        public override string Name
        {
            get { return "Wee bit of health"; }
        }

        public override int Amount
        {
            get { return 10; }
        }
    }
}
