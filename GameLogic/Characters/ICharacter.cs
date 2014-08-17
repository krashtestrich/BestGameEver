using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Arena;
using GameLogic.Enums;
using GameLogic.Modifiers;

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

        int BonusHealth { get; }
        int BaseHealth { get; }
        void AddBonusHealth(int amount);
        void SetHealth();
        void LoseHealth(int amount);
        void GainHealth(int amount);

        int Mana { get; }
        int BonusMana { get; }
        int BaseMana { get; }
        void AddBonusMana(int amount);
        void SetMana();
        void LoseMana(int amount);
        void GainMana(int amount);
        #endregion

        #region Alliance
        Alliance GetAlliance();
        void ChangeAlliance(Alliance a);
        #endregion

        #region Level
        int GetLevel();
        void SetLevel(int level);
        void LevelUp();
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
        List<IAction> CurrentAvailableActions { get; } 
        void TargetTile(ArenaFloorTile tile);
        List<IAction> TargetTileAndSelectActions(ArenaFloorTile tile);
        List<IAction> GetActions(bool canPerform);
        void UntargetTile();

        #endregion

        #region Cash
        int Cash { get; }

        void SetCash(int amount);
        void AddCash(int amount);

        #endregion

        #region SkillTree
        SkillTree.SkillTree SkillTree { get; }
        #endregion

        #region Modifiers 

        void AddModifier(IModifier<ICharacter> modifier);
        void RemoveModifier(IModifier<ICharacter> modifier);

        #endregion

        #region Shop / Buying
        void BuyItems();
        #endregion

        #region Arena / Game

        ArenaFloorTile ArenaLocation { get; }

        void LeaveArena();

        #endregion

    }
}
