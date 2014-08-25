using System.Linq;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests.Spells.Attacks
{
    [TestClass]
    public class WhenCastingAttackSpells
    {
        [TestMethod]
        public void ShouldHaveAttackSpellActionOnOpponent()
        {
            var g = new Game { Player = new Player() };
            g.Player.AddAction(new SpellSpear());
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo, 0, 2);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(c.ArenaLocation);
            Assert.IsTrue(actions.Exists(i => i is SpellSpear));
        }

        [TestMethod]
        public void ShouldNotHaveAttackSpellActionOnSelf()
        {
            var g = new Game { Player = new Player() };
            g.Player.AddAction(new SpellSpear());
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            Assert.IsFalse(actions.Exists(i => i is SpellSpear));
        }

        [TestMethod]
        public void ShouldDealDamageWhenAttackCast()
        {
            var g = new Game { Player = new Player() };
            g.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            g.Player.AddAction(new SpellSpear()); ;
            var c = new Dumbass();
            g.Arena.AddCharacterToArena(c, Alliance.TeamTwo, 0, 2);
            g.StartBattle(BattleMode.PlayerVsComputer);
            var actions = g.Player.TargetTileAndSelectActions(c.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is SpellSpear));
            Assert.IsTrue(c.Health < 100);
        }
    }
}
