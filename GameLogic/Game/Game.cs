using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;

namespace GameLogic.Game
{
    public class Game
    {
        private BattleStatus _battleStatus;
        private BattleMode _battleMode;
        private Alliance? _winningTeam;

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
        public void StartBattle(BattleMode mode)
        {
            _battleMode = mode;
            _battleStatus = BattleStatus.InBattle;
        }

        public void EndBattle(Alliance winningTeam)
        {
            _winningTeam = winningTeam;
            _battleStatus = BattleStatus.BattleOver;
        }

        public void ResetBattle()
        {
            _battleStatus = BattleStatus.NotStarted;
            _winningTeam = null;
        }

        public BattleStatus GetBattleStatus()
        {
            return _battleStatus;
        }

        public void UpdateBattleStatus()
        {
            var teamOneAlive = (Arena.Characters.Exists(i => i.GetAlliance() == Alliance.TeamOne && i.Health > 0));
            var teamTwoAlive = (Arena.Characters.Exists(i => i.GetAlliance() == Alliance.TeamTwo && i.Health > 0));
            if (teamOneAlive && teamTwoAlive)
            {
                return;
            }

            var winningTeam = teamOneAlive
                ? Alliance.TeamOne
                : Alliance.TeamTwo;
            EndBattle(winningTeam);
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

        public void PerformAITurn(Alliance alliance)
        {
            Arena.Characters.Where(i => i.GetAlliance() == alliance).ToList().ForEach(
                i =>
                {
                    if (_battleStatus != BattleStatus.InBattle)
                    {
                        return;
                    }
                    Arena.BotSelectTile(i, BotGetTarget(alliance));
                    i.UntargetTile();
                    UpdateBattleStatus();
                });
        }

        private ArenaFloorTile BotGetTarget(Alliance sourceAlliance)
        {
            //TODO: Intelligent targetting - ie. select from list of possible targets based on criteria.
            return _battleMode == BattleMode.ComputerVsComputer 
                ? Arena.Characters.First(i => i.GetAlliance() != sourceAlliance).ArenaLocation 
                : Player.ArenaLocation;
        }
        
        public void ProcessBattleOver()
        {
            if (GetBattleStatus() != BattleStatus.BattleOver)
            {
                throw new Exception("Uhh, the battle isn't over?");
            }

            //TODO: Implement logic for when player lost.
            if (_battleMode == BattleMode.ComputerVsComputer || Player.Health > 0)
            {
                var cash = Arena.Characters.Where(i => i is Bot && i.GetAlliance() != _winningTeam).Sum(i => ((Bot)i).Worth);
                Arena.Characters.Where(i => i.GetAlliance() == _winningTeam && i.Health > 0)
                    .ToList()
                    .ForEach(w =>
                    {
                        w.AddCash(cash);
                        w.LevelUp();
                    });
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
                new Dumbass()
            };
        }

        public void ChooseOpponent(Bot opponent)
        {
            Arena.AddCharacterToArena(opponent, Alliance.TeamTwo);
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
