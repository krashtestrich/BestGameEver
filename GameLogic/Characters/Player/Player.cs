namespace GameLogic.Characters.Player
{
    public sealed class Player : Character
    {
        public override int BaseHealth
        {
            get { return 100; }
        }

        public override int BaseMana
        {
            get { return 50; }
        }

        public Player()
        {
            SetLevel(1);
            AddCash(100);
        }
    }
}
