using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Helpers;

namespace GameLogic.Tournament
{
    public class Tournament
    {
        public List<Participant> Participants { get; set; }
        public TournamentMode TournamentMode;
        public TournamentStatus TournamentStatus;
        public Participant Winner;

        public Tournament()
        {
            Participants = new List<Participant>();
            TournamentMode = TournamentMode.PlayerVsComputer;
            TournamentStatus = TournamentStatus.NotStarted;
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
            while (Participants.Count < 50)
            {
                var c = new Dumbass();
                AddCharacterToTournament(c);
            }
        }

        public void Start()
        {
            Participants.Where(i => i.Status == ParticipantStatus.Active && i.Character.GetLevel() == 0)
                .ToList();
            for (var i = 0; i < Participants.Count; i++)
            {
                    Participants[i].Character.SetLevel(1);
                    Participants[i].Character.SetCash(100);
                    Participants[i].Character.BuyItems();
            }
               
            TournamentStatus = TournamentStatus.InProgress;
        }

        public void SetPlayerAsCombatant()
        {
            Participants.First(i => i.Character is Player).Status = ParticipantStatus.InBattle;
        }

        public Participant ChooseCombatant()
        {
            var activeParticipants = Participants.Where(i => i.Status == ParticipantStatus.Active).ToList();
            if (activeParticipants.Count == 0)
            {
                return null;
            }
            var lowestParticipantLevel = activeParticipants.OrderBy(i => i.Battles).First().Battles;
            var lowestParticipants =
                Participants.Where(i => i.Status == ParticipantStatus.Active && i.Battles == lowestParticipantLevel).ToList();
            return lowestParticipants.Count == 1
                ? lowestParticipants.First()
                : lowestParticipants[new ThreadSafeRandom().Next(0, lowestParticipants.Count)];
        }

        public void ProcessBattleResult(ICharacter winner, ICharacter loser)
        {
            var winningParticipant = Participants.First(i => i.Character.Name == winner.Name);
            winningParticipant.Battles++;
            winningParticipant.Status = ParticipantStatus.Active;
            var losingParticipant = Participants.First(i => i.Character.Name == loser.Name);
            losingParticipant.Battles++;
            losingParticipant.Status = ParticipantStatus.KnockedOut;
            UpdateTournamentStatus();
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
