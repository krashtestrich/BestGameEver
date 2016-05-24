using System.Linq;
using GameLogic.Actions.Spells.Heals;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using GameLogic.SkillTree.Paths.WizardPath;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests.Spells.Heals
{
    [TestClass]
    public class WhenCastingHeals
    {
        [TestMethod]
        public void ShouldHaveHealActionOnSelf()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.Player.LevelUp();
            g.Player.ChooseSkill(new PathOfTheWizard());
            g.Player.AddAction(new LittleHeal());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            Assert.IsTrue(actions.Exists(i => i is LittleHeal));
        }

        [TestMethod]
        public void ShouldHealSelfWhenHealCast()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.Player.LevelUp();
            g.Player.ChooseSkill(new PathOfTheWizard());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            g.Player.AddAction(new LittleHeal());
            var c = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is LittleHeal));
            Assert.IsTrue(g.Player.Health > 100);
        }

        [TestMethod]
        public void ShouldRemoveManaWhenSpellCast()
        {
            var g = new Game
            {
                CurrentBattleDetails = new BattleDetails
                {
                    BattleMode = BattleMode.PlayerVsComputer,
                    BattleStatus = BattleStatus.InBattle,
                    BattleTurn = Alliance.TeamOne
                },
                Player = new Player()
            };
            g.Player.LevelUp();
            g.Player.ChooseSkill(new PathOfTheWizard());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            g.Player.AddAction(new LittleHeal());
            var c = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is LittleHeal));
            Assert.IsTrue(g.Player.Mana < 100);
        }
    }
}
