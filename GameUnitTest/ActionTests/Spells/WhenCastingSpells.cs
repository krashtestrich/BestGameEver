using System.Linq;
using GameLogic.Actions.Spells.Heals;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests.Spells
{
    [TestClass]
    public class WhenCastingSpells
    {
        [TestMethod]
        public void ShouldHaveHealActionOnSelf()
        {
            var g = new Game {Player = new Player()};

            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            Assert.IsTrue(actions.Exists(i => i is HealBase));
        }

        [TestMethod]
        public void ShouldHealSelfWhenHealCast()
        {
            var g = new Game { Player = new Player() };
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is HealBase));
            Assert.IsTrue(g.Player.Health > 100);
        }
    }
}
