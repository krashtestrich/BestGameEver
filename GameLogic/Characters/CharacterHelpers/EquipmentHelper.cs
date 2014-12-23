using System;
using System.Linq;
using GameLogic.Equipment;

namespace GameLogic.Characters.CharacterHelpers
{
    public class EquipmentHelper
    {
        public static bool CanEquipEquipment(ICharacter character, IBuyableEquipment equipment)
        {
            var uniqueSlots = equipment.Slots.Distinct();

            foreach (var e in uniqueSlots)
            {
                if (character.Slots.Count(i => i.SlotFree && i.SlotType == e.SlotType) < equipment.Slots.Select(x => x.SlotType == e.SlotType).Count())
                {
                    return false;
                }
            }
            return true;
        }

        public static void EquipEquipment(ICharacter character, IBuyableEquipment equipment)
        {
            if (CanEquipEquipment(character, equipment))
            {
                character.CharacterEquipment.Add(equipment);
                foreach (var s in equipment.Slots)
                {
                    character.Slots.Find(sf => sf.SlotFree && sf.SlotType == s.SlotType).SetSlotFree(false, equipment.Name);
                }
                ((EquipmentBase)equipment).Modifiers.ForEach(character.AddModifier);

                character.SetAttributes();
            }
            else
            {
                throw new Exception();
            }
        }

        public static void UnEquipEquipment(ICharacter character, IBuyableEquipment equipment)
        {
            character.CharacterEquipment.Remove(equipment);
            foreach (var s in equipment.Slots)
            {
                character.Slots.Find(sf => !sf.SlotFree && sf.SlotType == s.SlotType).SetSlotFree(true, null);
            }

            character.SetArmor();
            character.SetBlockAmount();
        }

        public static bool CanAffordEquipment(ICharacter character, IBuyableEquipment e)
        {
            return (character.Cash >= e.Price);
        }

        public static void PurchaseEquipment(ICharacter character, IBuyableEquipment e)
        {
            if (CanAffordEquipment(character, e) && CanEquipEquipment(character, e))
            {
                character.AddCash(-e.Price);
                EquipEquipment(character, e);
            }
            else
            {
                throw new Exception("Player tried to purchase unaffordable or unwearable Equipment.");
            }
        }

        public static void SellEquipment(ICharacter character, IBuyableEquipment e)
        {
            character.AddCash((int)Math.Round(e.Price * 0.75, MidpointRounding.ToEven));
            UnEquipEquipment(character, e);
        }
    }
}
