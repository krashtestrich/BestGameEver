using System;

namespace GameLogic.Arena
{
    public class ArenaHelper
    {
        public static int GetDistanceBetweenFloorPositions(ArenaFloorPosition p1, ArenaFloorPosition p2)
        {
            return Convert.ToInt32(Math.Floor(Maths.MathematicalFunctions.PythagorusGetHypotenusLengthFromRightAngledLengths(p1.XCoord - p2.XCoord, p2.YCoord - p2.YCoord)));
        }
    }
}
