namespace GameLogic.Characters.Player
{
    public sealed class Player : Character
    {
        public override int BaseHealth
        {
            get { return 150; }
        }

        public override int BaseMana
        {
            get { return 0; }
        }

        public override int BaseHealthRegeneration
        {
            get { return 1; }
        }

        public Player()
        {
            SetLevel(1);
            AddCash(100);
        }
    }
}
