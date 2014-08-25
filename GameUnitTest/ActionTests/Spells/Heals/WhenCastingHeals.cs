using System.Linq;
using GameLogic.Actions.Spells.Heals;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests.Spells.Heals
{
    [TestClass]
    public class WhenCastingHeals
    {
        [TestMethod]
        public void ShouldHaveHealActionOnSelf()
        {
            var g = new Game {Player = new Player()};
            g.Player.AddAction(new LittleHeal());;
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            Assert.IsTrue(actions.Exists(i => i is LittleHeal));
        }

        [TestMethod]
        public void ShouldHealSelfWhenHealCast()
        {
            var g = new Game { Player = new Player() };
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            g.Player.AddAction(new LittleHeal()); ;
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is LittleHeal));
            Assert.IsTrue(g.Player.Health > 100);
        }

        [TestMethod]
        public void ShouldRemoveManaWhenSpellCast()
        {
            var g = new Game { Player = new Player() };
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            g.Player.AddAction(new LittleHeal()); ;
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is LittleHeal));
            Assert.IsTrue(g.Player.Mana < 50);
        }
    }
}
