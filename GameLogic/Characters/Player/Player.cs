namespace GameLogic.Characters.Player
{
    public class Player : Character
    {
        public override int BaseHealth
        {
            get { return 100; }
        }

        public Player()
        {
            SetCash(100);
        }
    }
}
