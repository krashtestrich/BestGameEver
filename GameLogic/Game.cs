using GameLogic.Characters.Player;

namespace GameLogic
{
    public class Game
    {
        protected Arena.Arena Arena;
        protected Player Player;

        protected void CreateArena()
        {
            Arena = new Arena.Arena(new Battle.Battle());
            Player = new Player();
        }
    }
}
