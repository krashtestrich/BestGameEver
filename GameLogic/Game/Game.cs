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
        public Bot ChosenOpponent;
        public Tournament.Tournament Tournament;
        public bool EnableLogging;
        public BattleDetails CurrentBattleDetails;

        public Game(bool? enableLogging = false)
        {
            EnableLogging = enableLogging.HasValue && enableLogging.Value;
            Tournament = new Tournament.Tournament(EnableLogging);
            CurrentBattleDetails = new BattleDetails();
        }

        public void StartComputerVsComputerGame()
        {
            Tournament.TournamentMode = TournamentMode.ComputerVsComputer;
            Tournament.Populate();
            Tournament.Start();
            CurrentBattleDetails = Tournament.GetNextBattleDetails();
            StartBattle();
        }

        public void SimulateComputerVsComputerBattle(Guid battleGuid)
        {
            CurrentBattleDetails = Tournament.GetBattleByGuid(battleGuid);
            StartBattle();
        }

        public void SimulateAllComputerBattles()
        {
            var battle = Tournament.GetNextBattleDetails();
            while (battle != null)
            {
                if (battle.BattleMode == BattleMode.ComputerVsComputer)
                {
                    CurrentBattleDetails = battle;
                    StartBattle();
                }
                battle = Tournament.GetNextBattleDetails();
            }
        }

        public void StartPlayerVsComputerTournament()
        {
            Tournament.TournamentMode = TournamentMode.PlayerVsComputer;
            Tournament.AddCharacterToTournament(Player);
            Tournament.Populate();
            Tournament.Start();
            CurrentBattleDetails = Tournament.GetNextBattleDetails();
            StartBattle();
        }

        private void StartBattle()
        {
            CurrentBattleDetails.BattleStatus = BattleStatus.InBattle;
            var characterOne = CurrentBattleDetails.Participants.ElementAt(0);
            var characterTwo = CurrentBattleDetails.Participants.ElementAt(1);
            CurrentBattleDetails.Arena.AddCharacterToArena(characterOne.Character, Alliance.TeamOne, 0, 4);
            CurrentBattleDetails.Participants.ElementAt(0).Status = ParticipantStatus.InBattle;
            CurrentBattleDetails.Arena.AddCharacterToArena(characterTwo.Character, Alliance.TeamTwo, 4, 0);
            CurrentBattleDetails.Participants.ElementAt(1).Status = ParticipantStatus.InBattle;

            CurrentBattleDetails.BattleTurn = Alliance.TeamOne;
            if (EnableLogging)
            {
                Logger.CreateBattleLog();
                Logger.WriteBattleTurnEntry(characterOne.Character.Name + " (" + characterOne.Character.CurrentClass + ") " + " vs. " + characterTwo.Character.Name + " (" + characterTwo.Character.CurrentClass + ") ");
            }

            if (CurrentBattleDetails.BattleMode == BattleMode.ComputerVsComputer)
            {
                var aiTurn = Alliance.TeamOne;
                while (CurrentBattleDetails.BattleStatus == BattleStatus.InBattle)
                {
                    PerformAITurn();
                    aiTurn = aiTurn == Alliance.TeamOne ? Alliance.TeamTwo : Alliance.TeamOne;
                }

                EndComputerVsComputerBattle();
            }
            
        }

        private void EndComputerVsComputerBattle()
        {
            if (Tournament.TournamentStatus == TournamentStatus.InProgress)
            {
                //CurrentBattleDetails = Tournament.GetNextBattleDetails();
                //StartBattle();
            }
        }

        public Player Player
        {
            get; set;
        }

        #region Battle Status

        public void EndBattle(Alliance winningTeam)
        {
            CurrentBattleDetails.WinningTeam = winningTeam;
            CurrentBattleDetails.WinnerName =
                CurrentBattleDetails.Participants.First(p => p.Character.GetAlliance() == winningTeam).Character.Name;
            CurrentBattleDetails.LoserName =
                CurrentBattleDetails.Participants.First(p => p.Character.GetAlliance() != winningTeam).Character.Name;
            CurrentBattleDetails.BattleStatus = BattleStatus.BattleOver;
            if (CurrentBattleDetails.BattleMode == BattleMode.ComputerVsComputer)
            {
                ProcessBattleOver();
            }
        }

        public void UpdateBattleStatus()
        {
            var teamOneAlive = (CurrentBattleDetails.Arena.Characters.Exists(i => i.GetAlliance() == Alliance.TeamOne && i.Health > 0));
            var teamTwoAlive = (CurrentBattleDetails.Arena.Characters.Exists(i => i.GetAlliance() == Alliance.TeamTwo && i.Health > 0));
            if (teamOneAlive && teamTwoAlive)
            {
                CurrentBattleDetails.BattleTurn = CurrentBattleDetails.BattleTurn == Alliance.TeamOne
                    ? Alliance.TeamTwo
                    : Alliance.TeamOne;
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
            if (CurrentBattleDetails.BattleStatus != BattleStatus.InBattle)
            {
                throw new Exception("The battle is over what the FUCK are you doing!?");
            }
            CurrentBattleDetails.TurnDetails.Add(a.Perform(Player));
            Player.UntargetTile();
            UpdateBattleStatus();
        }

        public void PerformAITurn()
        {
            CurrentBattleDetails.Arena.Characters.Where(i => i.GetAlliance() == CurrentBattleDetails.BattleTurn).ToList().ForEach(
                i =>
                {
                    if (CurrentBattleDetails.BattleStatus != BattleStatus.InBattle)
                    {
                        return;
                    }
                    var actionType = ActionChoosers.GetPreferredActionType(i);
                    var opponentTile = CurrentBattleDetails.Arena.Characters.First(c => c.GetAlliance() != i.GetAlliance()).ArenaLocation;
                    var action = ChooseAction(actionType, i, opponentTile);
                    if (action == null)
                    {
                        BotPerformMove(i, opponentTile);
                    }
                    else
                    {
                        if (EnableLogging)
                        {
                            var performedWith = action.PerformedWith == null ? "" : " with " + action.PerformedWith.Name;
                            Logger.WriteBattleTurnEntry(i.SkillTree.Get().Where(s => s.IsActive).OrderByDescending(s => s.Level).First().Path 
                                + " (" + i.Name + " " + i.Health + "/" + i.Mana +  ") " 
                                + "performed "
                                + action.Name
                                + performedWith);
                        }
                        CurrentBattleDetails.TurnDetails.Add(action.Perform((Character)i));
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
            var newTile = CurrentBattleDetails.Arena.ArenaFloor[newPosition.XCoord, newPosition.YCoord];
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
                                                + " (" + c.Name + " " + c.Health + "/" + c.Mana + ") "
                                                + "performed "
                                                + moveAction.Name);
                }
                CurrentBattleDetails.TurnDetails.Add(moveAction.Perform((Character)c));
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
            if (CurrentBattleDetails.BattleStatus != BattleStatus.BattleOver)
            {
                throw new Exception("Uhh, the battle isn't over?");
            }

            //TODO: Implement logic for when player lost.
            if (CurrentBattleDetails.BattleMode == BattleMode.ComputerVsComputer || Player.Health > 0)
            {
                var winner = CurrentBattleDetails.Arena.Characters.First(i => i.GetAlliance() == CurrentBattleDetails.WinningTeam);
                var loser = CurrentBattleDetails.Arena.Characters.First(i => i.GetAlliance() != CurrentBattleDetails.WinningTeam);
                Tournament.ProcessBattleResult(CurrentBattleDetails);
                var cash = CurrentBattleDetails.Arena.Characters.Where(i => i is Bot && i.GetAlliance() != CurrentBattleDetails.WinningTeam).Sum(i => ((Bot)i).Worth);
                CurrentBattleDetails.Arena.Characters.Where(i => i.GetAlliance() == CurrentBattleDetails.WinningTeam && i.Health > 0)
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

            CurrentBattleDetails.Arena.ResetArena();
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
            CurrentBattleDetails.Arena.AddCharacterToArena(opponent, Alliance.TeamTwo);
            ChosenOpponent = opponent;
        }

        #endregion


        public BattleReport BuildBattleReport(BattleDetails battleDetails)
        {
            var br = new BattleReport
            {
                BattleStatus = battleDetails.BattleStatus,
                PlayerWins = battleDetails.BattleStatus == BattleStatus.BattleOver && Player.Health > 0
            };
            return br;
        }
    }
}
