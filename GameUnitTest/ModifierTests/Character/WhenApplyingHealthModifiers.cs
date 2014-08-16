﻿using GameLogic.Characters.Player;
using GameLogic.Modifiers.Character.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ModifierTests.Character
{
    [TestClass]
    public class WhenApplyingHealthModifiers
    {
        [TestMethod]
        public void ShouldApplyPercentageHealth()
        {
            var p = new Player();
            p.AddModifier(new SuperHealth());
            p.SetHealth();
            Assert.IsTrue(p.Health == 120);
        }

        [TestMethod]
        public void ShouldApplyFlatHealth()
        {
            var p = new Player();
            p.AddModifier(new WeeBitOfHealth());
            p.SetHealth();
            Assert.IsTrue(p.Health == 110); 
        }
    }
}
