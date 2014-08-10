using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;

namespace GameLogic.Game
{
    public class Game
    {
        private BattleStatus _battleStatus;

        public Bot ChosenOpponent;
        public Arena.Arena Arena;

        public Game()
        {
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
            _battleStatus = BattleStatus.NotStarted;
        }

        public Player Player
        {
            get; set;
        }

        #region Battle Status
        public void StartBattle()
        {
            _battleStatus = BattleStatus.InBattle;
        }

        public void EndBattle()
        {
            _battleStatus = BattleStatus.BattleOver;
        }

        public void ResetBattle()
        {
            _battleStatus = BattleStatus.NotStarted;
        }

        public BattleStatus GetBattleStatus()
        {
            return _battleStatus;
        }

        public void UpdateBattleStatus()
        {
            if (Player.Health <= 0
                || !(Arena.Characters.Exists(i => i.GetAlliance() == Alliance.Opponent && i.Health > 0)))
            {
                EndBattle();
            }
        }
        #endregion

        #region Perform Actions/Turns
        public void PerformPlayerAction(IAction a)
        {
            if (_battleStatus != BattleStatus.InBattle)
            {
                throw new Exception("The battle is over what the FUCK are you doing!?");
            }
            a.PerformAction(Player);
            Player.UntargetTile();
            UpdateBattleStatus();
        }

        public void PerformOpponentTurn()
        {
            Arena.Characters.Where(i => i.GetAlliance() == Alliance.Opponent).ToList().ForEach(
                i =>
                {
                    if (_battleStatus != BattleStatus.InBattle)
                    {
                        return;
                    }
                    Arena.BotSelectTile(i, Player.ArenaLocation);
                    i.UntargetTile();
                    UpdateBattleStatus();
                });
        }

        public void ProcessBattleOver()
        {
            if (GetBattleStatus() != BattleStatus.BattleOver)
            {
                throw new Exception("Uhh, the battle isn't over?");
            }
            if (Player.Health > 0)
            {
                var cash = Arena.Characters.Where(i => i is Bot).Sum(i => ((Bot) i).Worth);
                Player.AddCash(cash);
                Player.LevelUp();
                Player.LeaveArena();
            }
            ResetBattle();            
            Arena.ResetArena();
        }

        #endregion

        #region Assign Opponent(s)
        public List<Bot> GetPossibleOpponents()
        {
            return new List<Bot>
            {
                new Dumbass(Alliance.Opponent, Player != null ? Player.GetLevel() : 1)
            };
        }

        public void ChooseOpponent(Bot opponent)
        {
            Arena.AddCharacterToArena(opponent);
            ChosenOpponent = opponent;
        }

        #endregion


        public BattleReport BuildBattleReport()
        {
            var br = new BattleReport
            {
                BattleStatus = _battleStatus,
                PlayerWins = _battleStatus == BattleStatus.BattleOver && Player.Health > 0
            };
            return br;
        }
    }
}
