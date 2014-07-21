using System;

namespace GameLogic.Arena
{
    public class ArenaHelper
    {
        public static int GetDistanceBetweenFloorPositions(ArenaFloorPosition p1, ArenaFloorPosition p2)
        {
            return Convert.ToInt32(Math.Floor(Maths.MathematicalFunctions.PythagorusGetHypotenusLengthFromRightAngledLengths(p1.XCoord - p2.XCoord, p1.YCoord - p2.YCoord)));
        }

        public static ArenaFloorPosition MovePositionDistanceCloser(ArenaFloorPosition p1, ArenaFloorPosition p2, int distance)
        {
            for (var i = 0; i < distance; i++)
            {
                if (p1.XCoord != p2.XCoord)
                {
                    p1.SetXCoord(p1.XCoord > p2.XCoord ? p1.XCoord - 1 : p1.XCoord + 1);
                }

                if (p1.YCoord == p2.YCoord) continue;
                p1.SetYCoord(p1.YCoord > p2.YCoord ? p1.YCoord - 1 : p1.YCoord + 1);
                
            }
            return p1;
        }
    }
}
