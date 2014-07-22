using System;

namespace GameLogic.Arena
{
    public class ArenaHelper
    {
        public static int GetDistanceBetweenFloorPositions(ArenaFloorPosition p1, ArenaFloorPosition p2)
        {
            return Convert.ToInt32(Math.Floor(Maths.MathematicalFunctions.PythagorusGetHypotenusLengthFromRightAngledLengths(p1.XCoord - p2.XCoord, p1.YCoord - p2.YCoord)));
        }

        public static ArenaFloorPosition GetClosestMovablePosition(ArenaFloorPosition p1, ArenaFloorPosition p2, int distance)
        {
            var position = new ArenaFloorPosition(p1.XCoord,p1.YCoord);
            for (var i = 0; i < distance; i++)
            {
                if (p1.XCoord != p2.XCoord)
                {
                    position.SetXCoord(position.XCoord > p2.XCoord ? position.XCoord - 1 : position.XCoord + 1);
                }

                if (p1.YCoord == p2.YCoord) continue;
                position.SetYCoord(position.YCoord > p2.YCoord ? position.YCoord - 1 : position.YCoord + 1);
                
            }
            return position;
        }
    }
}
