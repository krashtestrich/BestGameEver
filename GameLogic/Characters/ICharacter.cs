using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Arena;

namespace GameLogic.Characters
{
    public interface ICharacter
    {
        #region Basic - Name, etc.
        string Name
        { get; }
        void SetName(string name);
        #endregion

        #region Health, Mana, etc.
        int Health
        {
            get; 
        }
        void SetHealth(int health);
        void LoseHealth(int amount);
        void GainHealth(int amount);
        #endregion

        #region Slots
        List<Slot> Slots
        {
            get;
        }

        void AddSlot(Slot slot);
        #endregion

        #region Equipment
        List<Equipment.Equipment> CharacterEquipment
        {
            get;
        }

        bool CanEquipEquipment(Equipment.Equipment equipment);
        void EquipEquipment(Equipment.Equipment equipment);
        void UnEquipEquipment(Equipment.Equipment equipment);
        #endregion

        #region Actions

        List<IAction> SelectActionsFromTargetTile(ArenaFloorTile tile);
        void SetCharacterLocation(int x, int y);
        void PerformAction(IAction a);
        #endregion
    }
}
