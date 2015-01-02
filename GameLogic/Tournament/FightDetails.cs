using System;
using System.Collections.Generic;
using GameLogic.Enums;

namespace GameLogic.Tournament
{
    public class BattleDetails
    {
        public BattleDetails()
        {
            Participants = new List<Participant>();
            //TurnDetails = new List<TurnDetail>();
            TurnDetails = new List<string>();
            BattleStatus = BattleStatus.NotStarted;
            Arena = new Arena.Arena();
            Arena.BuildArenaFloor(5);
            BattleGuid = Guid.NewGuid();
        }
        public int Turn { get; set; }
        //public List<TurnDetail> TurnDetails { get; set; } 
        public List<string> TurnDetails { get; set; } 
        public List<Participant> Participants { get; set; }
        public Arena.Arena Arena { get; set; }
        public BattleMode BattleMode { get; set; }
        public BattleStatus BattleStatus { get; set; }
        public Alliance BattleTurn { get; set; }
        public Alliance WinningTeam { get; set; }
        public string WinnerName { get; set; }
        public string LoserName { get; set; }
        public Guid BattleGuid { get; set; }
    }
}
