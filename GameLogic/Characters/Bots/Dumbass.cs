namespace GameLogic.Characters.Bots
{
    public class Dumbass : Bot
    {
        public override int BaseHealth
        {
            get { return 100; }
        }

        public override int BaseMana
        {
            get { return 50; }
        }

        public override int Worth
        {
            get { return 50; }
        }
    }
}
