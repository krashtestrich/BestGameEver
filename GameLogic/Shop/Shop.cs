using System.Collections.Generic;
using GameLogic.Characters.Player;
using GameLogic.Equipment.Weapons;

namespace GameLogic.Shop
{
    public class Shop : IShop
    {
        #region Equipment
        private readonly List<Equipment.Equipment> _equipment;

        public List<Equipment.Equipment> Equipment
        {
            get
            {
                return _equipment;
            }
        }

        private void PopulateShop()
        {
            _equipment.Add(new Sword());
            _equipment.Add(new BigSword());
        }
        #endregion        

        #region Player

        public Player PlayerAtShop { get; private set; }

        public void AddPlayerToShop(Player p)
        {
            PlayerAtShop = p;
        }
        #endregion

        public Shop()
        {
            _equipment = new List<Equipment.Equipment>();
            PopulateShop();
        }
    }
}
