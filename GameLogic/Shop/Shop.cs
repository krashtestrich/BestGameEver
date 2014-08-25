using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters.Player;
using GameLogic.Equipment;
using GameLogic.Helpers;

namespace GameLogic.Shop
{
    public class Shop : IShop
    {
        #region Equipment
        private List<IBuyableEquipment> _equipment;

        public List<IBuyableEquipment> Equipment
        {
            get
            {
                return _equipment;
            }
        }

        private void PopulateShop()
        {
            var items = new InstanceRetriever<IBuyableEquipment>().RetrieveInstances();
            _equipment = items.ToList();
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
            _equipment = new List<IBuyableEquipment>();
            PopulateShop();
        }
    }
}
