namespace GameLogic.Characters.Player
{
    public interface IPlayer
    {
        int Cash
        { get; }

        void SetCash(int amount);
        void AddCash(int amount);
        bool CanAffordEquipment(Equipment.Equipment e);
        void PurchaseEquipment(Equipment.Equipment e);
        void SellEquipment(Equipment.Equipment e);
    }
}
