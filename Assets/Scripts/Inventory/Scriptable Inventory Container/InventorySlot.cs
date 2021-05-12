using System;
using UnityEngine;

namespace RogueLike.Inventory
{
   [System.Serializable]
    public class InventorySlot
    {
        public ScriptableItemType[] AllowedItems = new ScriptableItemType[0];

        [System.NonSerialized]
        public UserInterface parent;

        [System.NonSerialized]
        public GameObject SlotDisplay;

        public event Action<InventorySlot> OnAfterUpdate;
        public event Action<InventorySlot> OnBeforeUpdate;

        public Item item;
        
        public int amount;

        public ItemObject ItemObject
        {
            get
            {
                if (item.Id >= 0)
                {
                    return parent.inventory.database.ItemObjects[item.Id];
                }

                return null;
            }
        }

        public InventorySlot()
        {
            UpdateSlot(new Item(), 0);
        }

        public InventorySlot(Item _item, int _amount)
        {
            UpdateSlot(_item, _amount);
        }

        public void UpdateSlot(Item _item, int _amount)
        {
            if (OnBeforeUpdate != null)
            {
                OnBeforeUpdate.Invoke(this);
            }

            item = _item;
            amount = _amount;

            if (OnAfterUpdate != null)
            {
                OnAfterUpdate.Invoke(this);
            }
        }

        public void RemoveItem()
        {
            UpdateSlot(new Item(), 0);
        }
        
        public void AddAmount(int value)
        {
            UpdateSlot(item, amount += value);
        }
        
        public bool CanPlaceInSlot(ItemObject _itemObject)
        {
            if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
            {
                return true;
            }

            for (int i = 0; i < AllowedItems.Length; i++)
            {
                if (_itemObject.itemType == AllowedItems[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}