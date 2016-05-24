using System.Linq;
using GameLogic.Actions.Spells.Damage;
using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Bots.BotTypes;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Game;
using GameLogic.SkillTree.Paths.WizardPath;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ActionTests.Spells.Attacks
{
    [TestClass]
    public class WhenCastingAttackSpells
    {
        [TestMethod]
        public void ShouldHaveAttackSpellActionOnOpponent()
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
            g.Player.AddAction(new SpellSpear());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(c, Alliance.TeamTwo, 0, 2);
            var actions = g.Player.TargetTileAndSelectActions(c.ArenaLocation);
            Assert.IsTrue(actions.Exists(i => i is SpellSpear));
        }

        [TestMethod]
        public void ShouldNotHaveAttackSpellActionOnSelf()
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
            g.Player.AddAction(new SpellSpear());
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne);
            var c = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(c, Alliance.TeamTwo);
            var actions = g.Player.TargetTileAndSelectActions(g.Player.ArenaLocation);
            Assert.IsFalse(actions.Exists(i => i is SpellSpear));
        }

        [TestMethod]
        public void ShouldDealDamageWhenAttackCast()
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
            g.Player.AddAction(new SpellSpear());
            var c = new Dumbass();
            g.CurrentBattleDetails.Arena.AddCharacterToArena(c, Alliance.TeamTwo, 0, 2);
            var actions = g.Player.TargetTileAndSelectActions(c.ArenaLocation);
            g.PerformPlayerAction(actions.First(i => i is SpellSpear));
            Assert.IsTrue(c.Health < 100);
        }

        [TestMethod]
        public void ShouldAddMagicDamageToSpell()
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
            g.Player.SetName("Player");
            g.Player.LevelUp();
            g.Player.ChooseSkill(new PathOfTheWizard());
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Status = ParticipantStatus.InBattle,
                Character = g.Player
            });
            g.CurrentBattleDetails.Arena.BuildArenaFloor(10);
            g.Player.AddAction(new SpellSpear());
            g.Player.AddMagicDamage(100);
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            o.SetName("Dumbass");
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Status = ParticipantStatus.InBattle,
                Character = o
            });
            g.CurrentBattleDetails.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i is SpellSpear));
            Assert.IsTrue(o.Health < 0);
        }

        [TestMethod]
        public void ShouldAddMagicDamagePercentageToSpell()
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
            g.Player.SetName("Player");
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Status = ParticipantStatus.InBattle,
                Character = g.Player
            });
            g.CurrentBattleDetails.Arena.BuildArenaFloor(10);
            g.Player.AddAction(new SpellSpear());
            g.Player.AddMagicDamageBonusPercent(1000);
            g.CurrentBattleDetails.Arena.AddCharacterToArena(g.Player, Alliance.TeamOne, 0, 0);
            var o = new Dumbass();
            o.SetName("Dumbass");
            g.CurrentBattleDetails.Participants.Add(new Participant
            {
                Status = ParticipantStatus.InBattle,
                Character = o
            });

            g.CurrentBattleDetails.Arena.AddCharacterToArena(o, Alliance.TeamTwo, 0, 1);
            var tile = g.CurrentBattleDetails.Arena.SelectFloorTile(new ArenaFloorPosition(0, 1));
            var actions = g.Player.TargetTileAndSelectActions(tile);
            g.PerformPlayerAction(actions.Find(i => i is SpellSpear));
            Assert.IsTrue(o.Health < 0);
        }
    }
}
