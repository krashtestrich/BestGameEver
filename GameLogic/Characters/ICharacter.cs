using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Arena;
using GameLogic.Enums;
using GameLogic.Equipment;
using GameLogic.Modifiers;
using GameLogic.SkillTree.Paths;

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
        int BaseHealthRegeneration { get; }
        int BonusHealthRegeneration { get; }
        void AddBonusHealth(int amount);
        void SetHealth();
        void LoseHealth(int amount);
        void GainHealth(int amount);
        void AddBonusHealthRegeneration(int amount);


        int Mana { get; }
        int BonusMana { get; }
        int BaseMana { get; }
        void AddBonusMana(int amount);
        void SetMana();
        void LoseMana(int amount);
        void GainMana(int amount);

        int Armor { get; }
        int BonusArmor { get; }
        int BaseArmor { get; }
        void AddBonusArmor(int amount);
        void SetArmor();
        void LoseArmor(int amount);
        void GainArmor(int amount);
        #endregion

        #region Physical Damage

        int PhysicalDamage { get; }
        int PhysicalDamageBonusPercent { get; }
        void AddPhysicalDamage(int amount);
        void AddPhysicalDamageBonusPercent(int amount);
        void SetPhysicalDamage();

        #endregion

        #region Magic Damage
        int MagicDamage { get; }
        int MagicDamageBonusPercent { get; }
        void AddMagicDamage(int amount);
        void AddMagicDamageBonusPercent(int amount);
        void SetMagicDamage();
        #endregion

        #region Block
        int BlockAmount { get; }
        int BonusBlockAmount { get; }
        int BaseBlockAmount { get; }
        void AddBonusBlockAmount(int amount);
        void SetBlockAmount();
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
        List<IBuyableEquipment> CharacterEquipment
        {
            get;
        }
        #endregion

        #region Actions
        List<IAction> CurrentAvailableActions { get; } 
        void TargetTile(ArenaFloorTile tile);
        List<IAction> TargetTileAndSelectActions(ArenaFloorTile tile);
        List<IAction> GetActions(bool canPerform);
        void UntargetTile();
        void AddAction(IAction action);

        #endregion

        #region Cash
        int Cash { get; }

        void SetCash(int amount);
        void AddCash(int amount);

        #endregion

        #region SkillTree
        SkillTree.SkillTree SkillTree { get; }
        SkillBranches? CurrentClass { get; }
        IPath CurrentPath { get; }

        #endregion

        #region Modifiers 

        void AddModifier(IModifier<ICharacter> modifier);

        #endregion

        #region Shop / Buying
        void BuyItems();
        #endregion

        #region Arena / Game

        ArenaFloorTile ArenaLocation { get; }

        void LeaveArena();

        #endregion

        void SetAttributes();

    }
}
