using System;

namespace GameLogic.Game
{
    public class GameHelper
    {
        [ThreadStatic]
        public static Random R;
    }
}
