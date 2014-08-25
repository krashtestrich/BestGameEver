using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Actions.Attacks;
using GameLogic.Actions.Movements;
using GameLogic.Actions.Spells;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Actions.Spells.Heals;
using GameLogic.AI;
using GameLogic.Arena;
using GameLogic.Characters;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Helpers;
using GameLogic.Tournament;

namespace GameLogic.Game
{
    public class Game
    {
        private BattleStatus _battleStatus;
        private BattleMode _battleMode;
        private Alliance? _winningTeam;

        public Bot ChosenOpponent;
        public Arena.Arena Arena;
        public Tournament.Tournament Tournament;
        public bool EnableLogging;

        public Game(bool? enableLogging = false)
        {
            EnableLogging = enableLogging.HasValue && enableLogging.Value;
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
            _battleStatus = BattleStatus.NotStarted;
            Tournament = new Tournament.Tournament(EnableLogging);
        }

        public void StartComputerVsComputerGame()
        {
            Tournament.TournamentMode = TournamentMode.ComputerVsComputer;
            Tournament.Populate();
            Tournament.Start();
            StartComputerVsComputerBattle();
        }

        public void StartPlayerVsComputerGame()
        {
            Tournament.TournamentMode = TournamentMode.PlayerVsComputer;
            Tournament.AddCharacterToTournament(Player);
            Tournament.Populate();
            Tournament.Start();
            StartPlayerVsComputerBattle();
        }

        private void StartPlayerVsComputerBattle()
        {
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
            _battleStatus = BattleStatus.InBattle;
            Tournament.SetPlayerAsCombatant();
            Arena.AddCharacterToArena(Player, Alliance.TeamOne);
            var c2 = Tournament.ChooseCombatant();
            if (c2 == null)
            {
                throw new Exception("Combatant is null....");
            }
            c2.Status = ParticipantStatus.InBattle;
            Arena.AddCharacterToArena(c2.Character, Alliance.TeamTwo);
        }

        private void StartComputerVsComputerBattle()
        {
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
            _battleStatus = BattleStatus.InBattle;
            var c1 = Tournament.ChooseCombatant();
            if (c1 == null)
            {
                throw new Exception("Combatant is null....");
            }
            c1.Status = ParticipantStatus.InBattle;
            var c2 = Tournament.ChooseCombatant();
            if (c2 == null)
            {
                throw new Exception("Combatant is null....");
            }
            c2.Status = ParticipantStatus.InBattle;
            Arena.AddCharacterToArena(c1.Character, Alliance.TeamOne, 0, 0);
            Arena.AddCharacterToArena(c2.Character, Alliance.TeamTwo);
            StartBattle(BattleMode.ComputerVsComputer);
            if (EnableLogging)
            {
                Logger.WriteBattleTurnEntry(c1.Character.Name + " (" + c1.Character.CurrentClass + ") " + " vs. " + c2.Character.Name + " (" + c2.Character.CurrentClass + ") ");
            }
            var aiTurn = Alliance.TeamOne;
            while (_battleStatus == BattleStatus.InBattle)
            {
                PerformAITurn(aiTurn);
                aiTurn = aiTurn == Alliance.TeamOne ? Alliance.TeamTwo : Alliance.TeamOne;
            }

            EndComputerVsComputerBattle();
        }

        private void EndComputerVsComputerBattle()
        {
            if (Tournament.TournamentStatus == TournamentStatus.InProgress)
            {
                StartComputerVsComputerBattle();
            }
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
            if (EnableLogging)
            {
                Logger.CreateBattleLog();
            }
        }

        public void EndBattle(Alliance winningTeam)
        {
            _winningTeam = winningTeam;
            _battleStatus = BattleStatus.BattleOver;
            if (_battleMode == BattleMode.ComputerVsComputer)
            {
                ProcessBattleOver();
            }
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
            a.Perform(Player);
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
                    var actionType = ActionChoosers.GetPreferredActionType(i);
                    var opponentTile = Arena.Characters.First(c => c.GetAlliance() != i.GetAlliance()).ArenaLocation;
                    var action = ChooseAction(actionType, i, opponentTile);
                    if (action == null)
                    {
                        BotPerformMove(i, opponentTile);
                    }
                    else
                    {
                        if (EnableLogging)
                        {
                            Logger.WriteBattleTurnEntry(i.SkillTree.Get().Where(s => s.IsActive).OrderByDescending(s => s.Level).First().Path 
                                + " (" + i.Name + ") " 
                                + "performed "
                                + action.Name);
                        }
                        action.Perform((Character)i);
                    }
                    i.UntargetTile();
                    UpdateBattleStatus();
                });
        }

        private void BotPerformMove(ICharacter c, ArenaFloorTile tile)
        {
            // Get movement with most distance.
            var moveAction = c.GetActions(false)
                .Where(a => a is MoveBase)
                .OrderByDescending(a => ((MoveBase)a).Distance)
                .FirstOrDefault() as MoveBase;

            if (moveAction == null) return;
            // Get as close to player as possible - find movement that does this.
            var d = moveAction.Distance;
            var newPosition = ArenaHelper.GetClosestMovablePosition(c.ArenaLocation.GetTileLocation(), tile.GetTileLocation(), d);
            var newTile = Arena.ArenaFloor[newPosition.XCoord, newPosition.YCoord];
            //TODO: Implement logic for bot moving around obstacles? For now don't allow movement onto tiles that have entities
            var actions = c.TargetTileAndSelectActions(newTile);
            if (actions.Exists(i => i.Name == moveAction.Name))
            {
                if (EnableLogging)
                {
                    Logger.WriteBattleTurnEntry(c.SkillTree.Get()
                        .Where(s => s.IsActive)
                        .OrderByDescending(s => s.Level)
                        .First()
                        .Path
                                                + " (" + c.Name + ") "
                                                + "performed "
                                                + moveAction.Name);
                }
                moveAction.Perform((Character)c);
            }
        }

        private readonly Dictionary<Type, Func<ICharacter, ArenaFloorTile, IAction>> _actionChoosersByType = new Dictionary<Type, Func<ICharacter, ArenaFloorTile, IAction>>
        {
            {
                typeof(HealBase), (c,t) => c.TargetTileAndSelectActions(c.ArenaLocation)
                    .Where(i => i is HealBase)
                    .Cast<ISpell>()
                    .ToList()
                    .OrderByDescending(i => i.HitsForTo)
                    .Cast<IAction>()
                    .FirstOrDefault()
            },
            {
                typeof(AttackBase), (c,t) => c.TargetTileAndSelectActions(t)
                    .Where(i => i is AttackBase)
                    .Cast<IAttack>()
                    .ToList()
                    .OrderByDescending(i => i.DamageToModifier)
                    .Cast<IAction>()
                    .FirstOrDefault()
            },
            {
                typeof(DamageBase), (c,t) => c.TargetTileAndSelectActions(t)
                    .Where(i => i is DamageBase)
                    .Cast<ISpell>()
                    .ToList()
                    .OrderByDescending(i => i.HitsForTo)
                    .Cast<IAction>()
                    .FirstOrDefault()
            }
        }; 
        private IAction ChooseAction(Type actionType, ICharacter c, ArenaFloorTile opponentTile)
        {
            if (!_actionChoosersByType.ContainsKey(actionType))
            {
                throw new Exception("Error - you can't choose an action for that action type!");
            }
            return _actionChoosersByType[actionType].Invoke(c, opponentTile);
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
                var winner = Arena.Characters.First(i => i.GetAlliance() == _winningTeam);
                var loser = Arena.Characters.First(i => i.GetAlliance() != _winningTeam);
                Tournament.ProcessBattleResult(winner, loser);
                var cash = Arena.Characters.Where(i => i is Bot && i.GetAlliance() != _winningTeam).Sum(i => ((Bot)i).Worth);
                Arena.Characters.Where(i => i.GetAlliance() == _winningTeam && i.Health > 0)
                    .ToList()
                    .ForEach(w =>
                    {
                        w.SetHealth();
                        w.SetMana();
                        w.SetArmor();
                        w.AddCash(cash);
                        w.LevelUp();
                    });

                if (EnableLogging)
                {
                    Logger.WriteBattleTurnEntry("Winner: " + winner.Name + " (" + winner.CurrentClass + ")");
                }
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
