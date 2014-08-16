using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters;
using GameLogic.Characters.Bots;

namespace GameLogic.Tournament
{
    public class Tournament
    {
        public List<Participant> Participants { get; set; }

        public Tournament()
        {
            Participants = new List<Participant>();
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
            Participants.Where(i => i.Status == ParticipantStatus.Active)
                .ToList()
                .ForEach(i =>
                {
                    i.Character.SetCash(100);
                    i.Character.SetLevel(1);
                });
        }
    }
}
