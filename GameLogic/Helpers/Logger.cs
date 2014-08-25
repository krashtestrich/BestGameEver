using System;
using System.IO;

namespace GameLogic.Helpers
{
    public class Logger
    {
        private const string LogBaseLocation = "D:\\GameOutput\\";
        private static string _tournamentLogLocation;
        private static string _currentBattleLog;
        private static string _currentTournamentLog;
        private static int _battleCount;

        public Logger()
        {
            var folderName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            _tournamentLogLocation = LogBaseLocation + folderName;
            Directory.CreateDirectory(LogBaseLocation + folderName);
            _currentTournamentLog = _tournamentLogLocation + "\\Results.txt";
        }

        public static void CreateBattleLog()
        {
            _currentBattleLog = _tournamentLogLocation + "\\" + _battleCount.ToString("0000") + ".txt";
            _battleCount++;
        }

        public static void WriteBattleResult(string result)
        {
            using (var w = File.AppendText(_currentTournamentLog))
            {
                w.WriteLine(result);
            }
        }

        public static void WriteBattleTurnEntry(string entry)
        {
            using (var w = File.AppendText(_currentBattleLog))
            {
                w.WriteLine(entry);
            }
        }
    }
}
