using GameLogic.Characters;

namespace GameLogic.Tournament
{
    public class Participant
    {
        public ICharacter Character { get; set; }
        public ParticipantStatus Status { get; set; }
        public int Battles { get; set; }

        public Participant()
        {
            Status = ParticipantStatus.Active;
        }
    }
}
