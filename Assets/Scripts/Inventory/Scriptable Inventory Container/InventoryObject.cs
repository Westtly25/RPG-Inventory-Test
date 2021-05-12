using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using RogueLike.Save;

namespace RogueLike.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Container", menuName = "Rogue Like Prototype/Inventory/Inventory Container/ New Inventory Container", order = 0)]
    public class InventoryObject : ScriptableObject, ISaveable
    {
        [Header("INVENTORY DATA BASE")]
        public ItemDatabaseObject database;

        [Header("INVENTORY")]
        public Inventory Container;

        public InventorySlot[] GetSlots
        {
            get
            {
                return Container.Slots;
            }
        }

        public InterfaceType interfaceType;

        public bool AddItem(Item _item, int _amount)
        {
            if (EmptySlotsCount <= 0)
            {
                return false;
            }

            InventorySlot slot = FindItemOnInventory(_item);

            if (!database.ItemObjects[_item.Id].stackable || slot == null)
            {
                SetEmptySlot(_item, _amount);
                return true;
            }

            slot.AddAmount(_amount);
            return true;
        }

        public int EmptySlotsCount
        {
            get
            {
                int counter = 0;

                for (int i = 0; i < GetSlots.Length; i++)
                {
                    if (GetSlots[i].item.Id <= -1)
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }

        public InventorySlot FindItemOnInventory(Item _item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id == _item.Id)
                {
                    return GetSlots[i];
                }
            }

            return null;
        }

        public InventorySlot SetEmptySlot(Item _item, int _amount)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                {
                    GetSlots[i].UpdateSlot(_item, _amount);
                    return GetSlots[i];
                }
            }
            return null;
        }

        public void SwapItems(InventorySlot item1, InventorySlot item2)
        {
            if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            {
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            }
        }

        public void RemoveItem(Item _item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item == _item)
                {
                    GetSlots[i].UpdateSlot(null, 0);
                }
            }
        }

        [ContextMenu("Save")]
        public async void Save()
        {
            SaveData.Current.Inventory = Container;
            await SaveSystem.SaveAsync();
        }

        [ContextMenu("Load")]
        public async void Load()
        {
            await SaveSystem.LoadAsync();

            Inventory loaded = SaveData.Current.Inventory;

            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(loaded.Slots[i].item, loaded.Slots[i].amount);
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();
        }
    }
}