using GameLogic.Arena;
using GameLogic.Characters.Bots;
using GameLogic.Characters.Player;
using GameLogic.Enums;
using GameLogic.Equipment.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.ArenaTests
{
    [TestClass]
    public class WhenAddingCharactersToArena
    {
        [TestMethod]
        public void ShouldAddDumbassBotToArena()
        {
            var arena = new Arena();
            arena.BuildArenaFloor(5);
            var p = new Player();
            p.EquipEquipment(new Sword());
            arena.AddCharacterToArena(p, Alliance.TeamOne);
            var b = new Dumbass();
            b.SetName("Dumbass Bot");
            b.EquipEquipment(new Sword());
            arena.AddCharacterToArena(b, Alliance.TeamTwo);
        }
    }
}
