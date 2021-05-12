using System;

namespace RogueLike.Inventory
{
    [Serializable]
    public class Inventory
    {
        public InventorySlot[] Slots = new InventorySlot[35];
        
        public void Clear()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].RemoveItem();
            }
        }
    }
}