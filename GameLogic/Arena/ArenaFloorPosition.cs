namespace GameLogic.Arena
{
    public class ArenaFloorPosition
    {
        public int XCoord { get; private set; }

        public void SetXCoord(int x)
        {
            XCoord = x;
        }

        public int YCoord { get; private set; }

        public void SetYCoord(int y)
        {
            YCoord = y;
        }

        public ArenaFloorPosition(int x, int y)
        {
            XCoord = x;
            YCoord = y;
        }
    }
}
