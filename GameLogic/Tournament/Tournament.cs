using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Helpers;
using Dumbass = GameLogic.Characters.Bots.BotTypes.Dumbass;

namespace GameLogic.Tournament
{
    public class Tournament
    {
        public List<Participant> Participants { get; set; }
        public TournamentMode TournamentMode;
        public TournamentStatus TournamentStatus;
        public int Round;
        public Participant Winner;
        public Logger Logger;
        public bool EnableLogging;
        public Dictionary<int, List<BattleDetails>> BattlesByRound; 

        public Tournament(bool? enableLogging = false)
        {
            EnableLogging = enableLogging.HasValue && enableLogging.Value;
            if (EnableLogging)
            {
                Logger = new Logger();
            }
            Participants = new List<Participant>();
            TournamentMode = TournamentMode.PlayerVsComputer;
            TournamentStatus = TournamentStatus.NotStarted;
            BattlesByRound = new Dictionary<int, List<BattleDetails>>();
            Round = 0;
        }

        public BattleDetails GetNextBattleDetails()
        {
            return BattlesByRound[Round].FirstOrDefault(b => b.BattleStatus == BattleStatus.NotStarted);
        }

        public BattleDetails GetBattleByGuid(Guid battleGuid)
        {
            return BattlesByRound[Round].FirstOrDefault(b => b.BattleGuid == battleGuid);
        }

        public void SimulateBattle(BattleDetails battleDetails)
        {
            
        }

        public void AddCharacterToTournament(ICharacter c)
        {
            while (true)
            {
                if (Participants.Exists(i => i.Character.Name == c.Name))
                {
                    c.SetName(BotHelper.GenerateRandomBotName());
                    continue;
                }

                Participants.Add(new Participant
                {
                    Battles = 0, Character = c, Status = ParticipantStatus.Active
                });
                break;
            }
        }

        public void Populate()
        {
            while (Participants.Count < 128)
            {
                var c = new Dumbass();
                AddCharacterToTournament(c);
            }
        }

        public void Start()
        {
            Participants
                .ToList()
                .ForEach(p =>
                {
                    if (!(p.Character is Player))
                    {
                        p.Character.LevelUp();
                        p.Character.AddCash(100);
                        p.Character.BuyItems();
                    }
                });
               
            TournamentStatus = TournamentStatus.InProgress;
            StartNextRound();
        }

        private void StartNextRound()
        {
            Round++;
            PopulateFights();
        }

        private void PopulateFights()
        {
            BattlesByRound.Add(Round, new List<BattleDetails>());
            var takenParticipants = 0;
            var validParticipants = Participants.Where(p => p.Status == ParticipantStatus.Active).ToList();
            if (validParticipants.Count < 2)
            {
                throw new Exception("Not enough valid participants to fight bro");
            }
            while (takenParticipants < Participants.Count)
            {
                var battleDetails = new BattleDetails();
                battleDetails.Participants.Add(validParticipants.ElementAt(takenParticipants));
                takenParticipants++;
                battleDetails.Participants.Add(validParticipants.ElementAt(takenParticipants));
                takenParticipants++;
                battleDetails.BattleMode = battleDetails.Participants.Any(p => p.Character is Player)
                    ? BattleMode.PlayerVsComputer
                    : BattleMode.ComputerVsComputer;
                BattlesByRound[Round].Add(battleDetails);
            }
        }

        public void SetPlayerAsCombatant()
        {
            Participants.First(i => i.Character is Player).Status = ParticipantStatus.InBattle;
        }

        public void ProcessBattleResult(BattleDetails battleDetails)
        {
            var winningParticipant = Participants.First(i => i.Character.Name == battleDetails.WinnerName);
            winningParticipant.Battles++;
            winningParticipant.Status = ParticipantStatus.Active;
            var losingParticipant = Participants.First(i => i.Character.Name == battleDetails.LoserName);
            losingParticipant.Battles++;
            losingParticipant.Status = ParticipantStatus.KnockedOut;
            UpdateTournamentStatus();
            if (EnableLogging)
            {
                Logger.WriteBattleResult(
                    winningParticipant.Character.SkillTree.Get().Where(i => i.IsActive).OrderByDescending(i => i.Level).First().Path 
                    + " defeated " 
                    + losingParticipant.Character.SkillTree.Get().Where(i => i.IsActive).OrderByDescending(i => i.Level).First().Path ); 
            }
        }

        private void UpdateTournamentStatus()
        {
            if (Participants.Count(i => i.Status == ParticipantStatus.Active) == 1)
            {
                TournamentStatus = TournamentStatus.Finished;
                Winner = Participants.First(i => i.Status == ParticipantStatus.Active);
            }
        }
    }
}
