using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Enums;

namespace GameLogic.Equipment
{
    public interface IBuyableEquipment
    {
        string Name { get; }
        EquipmentType EquipmentType { get; }
        List<EquipmentSubType> EquipmentSubTypes { get; } 
        int Price { get; }
        List<Slot> Slots { get; }
        List<IAction> Actions { get; }
    }
}
