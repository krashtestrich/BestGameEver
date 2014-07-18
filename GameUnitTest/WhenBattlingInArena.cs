using GameLogic.Arena;
using GameLogic.Battle;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest
{
    [TestClass]
    public class WhenBattlingInArena
    {
        [TestMethod]
        public void ShouldAddDumbassBotToArena()
        {
            var arena = new Arena(new Battle());
            arena.BuildArenaFloor(5);
            var p = new Player();
            p.EquipEquipment(new Sword());
            arena.AddCharacterToArena(p);

            var b = new Dumbass(Alliance.Opponent);
            b.SetName("Dumbass Bot");
            b.EquipEquipment(new Sword());
            arena.AddCharacterToArena(b);
        }
    }
}
