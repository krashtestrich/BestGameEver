using System.Collections.Generic;
using GameLogic.Actions;

namespace GameLogic.Equipment
{
    public interface IBuyableEquipment
    {
        string Name { get; }
        string EquipmentType { get; }
        int Price { get; }
        List<Slot> Slots { get; }
        List<IAction> Actions { get; }
    }
}
