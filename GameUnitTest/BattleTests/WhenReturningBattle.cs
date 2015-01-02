using System;
using System.Linq;
using GameLogic.Tournament;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.BattleTests
{
    [TestClass]
    public class WhenReturningBattle
    {
        [TestMethod]
        public void ShouldReturnNullBattleForUnmatchedGuid()
        {
            var t = new Tournament();
            t.Populate();
            t.Start();
            var guid = Guid.NewGuid();
            Assert.IsNull(t.GetBattleByGuid(guid));
        }

        [TestMethod]
        public void ShouldReturnBattleByGuid()
        {
            var t = new Tournament();
            t.Populate();
            t.Start();
            var guid = t.BattlesByRound[t.Round].First().BattleGuid;
            var battle = t.GetBattleByGuid(guid);
            Assert.IsNotNull(battle);
            Assert.IsTrue(battle.BattleGuid == guid);
        }
    }
}
