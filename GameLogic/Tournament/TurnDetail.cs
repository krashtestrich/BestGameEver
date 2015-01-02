using GameLogic.Enums;

namespace GameLogic.Tournament
{
    public class TurnDetail
    {
        public int Turn { get; set; }
        public Alliance Alliance { get; set; }
        public string CharacterName { get; set; }
        public string TargetName { get; set; }
        public string ActionName { get; set; }
    }
}
