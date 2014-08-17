using System;
using System.Collections.Generic;
using GameLogic.Helpers;

namespace GameLogic.Characters.Bots
{
    public class BotHelper
    {
        private static readonly List<string> FirstNames = new List<string>
        {
            "Captain","Itchy","Sexy","Handsome","Violent","Sweet","Rocky","Weeping","Twisted",
            "Unknown","Mr","Mrs","King","Queen","Prince","Princess","Mister","Master","Monster",
            "Erect","Flacid","Flakey","Articulate","Dedicated","Strong","Smelly","Stinky","Vacuous",
            "Explicit","Scary","Horrifying","Bouncing","Beautiful","Child","Homosexual","Homo",
            "Hetero","Heterosexual","Asexual","Slave","Sleepy","Bashful","Embarassed","Messy"
        };

        private static readonly List<string> LastNames = new List<string>
        {
            "Amazing","Killer","Bear","Wolf","Zombie","Strangler","Murder","Champion","Captain",
            "Sex","Erection","Breast","Model","Rockstar","Alchemist","Wizard","Cleric","Pornstar",
            "Adult","Peanut","Vitamin","Spunk","Semen","Teacher","Doctor","Dentist","Businessman",
            "Shapeshifter","Idiot","Genius","Bot","Player","Opponent","Computer","Molester",
            "Slave","Master","Drummer","Guitarist","Bassist","Musician","Sleeper","Thinker"
        };


        public static string GenerateRandomBotName()
        {
            var r = new Random();
            return FirstNames[new ThreadSafeRandom().Next(0, FirstNames.Count)] + " " + LastNames[new ThreadSafeRandom().Next(0, LastNames.Count)];
        }
    }
}
