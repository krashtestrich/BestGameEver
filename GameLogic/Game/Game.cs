using System.Collections.Generic;
using GameLogic.Characters.Bots;
using GameLogic.Enums;

namespace GameLogic.Game
{
    public class Game
    {
        private GameStatus _gameStatus;

        public Bot ChosenOpponent;
        public Arena.Arena Arena;

        public Game()
        {
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
            _gameStatus = GameStatus.NotStarted;
        }

        #region Battle Status
        public void StartBattle()
        {
            _gameStatus = GameStatus.InBattle;
        }

        public void EndBattle()
        {
            _gameStatus = GameStatus.BattleOver;
        }

        public GameStatus GetGameStatus()
        {
            return _gameStatus;
        }
        #endregion

        #region Assign Opponent(s)
        public List<Bot> GetPossibleOpponents()
        {
            return new List<Bot>
            {
                new Dumbass(Alliance.Opponent)
            };
        }

        public void ChooseOpponent(Bot opponent)
        {
            Arena.AddCharacterToArena(opponent);
            ChosenOpponent = opponent;
        }

        #endregion
    }
}
