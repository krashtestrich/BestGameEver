﻿using System;
using GameLogic.Enums;

namespace GameLogic.Characters.Player
{
    public class Player : Character
    {
        public void AddCash(int amount)
        {
            Cash += amount;
        }

        public void LeaveArena()
        {
            SetHealth(100);
            SetEntityLocation(null);
        }

        #region Shop Stuff
        public bool CanAffordEquipment(Equipment.Equipment e)
        {
            return (Cash >= e.Price);
        }

        public void PurchaseEquipment(Equipment.Equipment e)
        {
            if (CanAffordEquipment(e) && CanEquipEquipment(e))
            {
                Cash -= e.Price;
                EquipEquipment(e);
            }
            else
            {
                throw new Exception("Player tried to purchase unaffordable or unwearable Equipment.");
            }
        }

        public void SellEquipment(Equipment.Equipment e)
        {
            Cash += (int)Math.Round(e.Price * 0.75, MidpointRounding.ToEven);
            UnEquipEquipment(e);
        }
        #endregion

        public Player() : base(Alliance.Player, 1)
        {
            SetHealth(100);
            SetCash(100);
        }
    }
}
