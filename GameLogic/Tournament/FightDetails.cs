using System.Collections.Generic;
using GameLogic.Enums;

namespace GameLogic.Tournament
{
    public class BattleDetails
    {
        public BattleDetails()
        {
            Participants = new List<Participant>();
            BattleStatus = BattleStatus.NotStarted;
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
        }
        public List<Participant> Participants { get; set; }
        public Arena.Arena Arena { get; set; }
        public BattleMode BattleMode { get; set; }
        public BattleStatus BattleStatus { get; set; }
        public Alliance BattleTurn { get; set; }
        public Alliance WinningTeam { get; set; }
        public string WinnerName { get; set; }
        public string LoserName { get; set; }
    }
}
