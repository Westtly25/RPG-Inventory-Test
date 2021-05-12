using UnityEngine;
using RogueLike.QuestSystem;
using RogueLike.EventsSO;

namespace RogueLike.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryObject[] inventories;

        #region Scriptable Events
        [SerializeField] private ItemEventSO addItemEvent;
        [SerializeField] private ItemEventSO dropItemEvent;
        [SerializeField] private ItemEventSO sellItemEvent;
        #endregion

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            if(addItemEvent != null)
            {
                addItemEvent.OnEventRised += AddItemToInventory;
            }

            if(dropItemEvent != null)
            {
                addItemEvent.OnEventRised += DropItemFromInventory;
            }

            if(sellItemEvent != null)
            {
                addItemEvent.OnEventRised += SellItemFromInventory;
            }
        }

        private void OnDisable()
        {
            if(addItemEvent != null)
            {
                addItemEvent.OnEventRised -= AddItemToInventory;
            }

            if(dropItemEvent != null)
            {
                addItemEvent.OnEventRised -= DropItemFromInventory;
            }

            if(sellItemEvent != null)
            {
                addItemEvent.OnEventRised -= SellItemFromInventory;
            }
        }

        private void AddItemToInventory(ItemObject item)
        {
            Debug.LogWarning($"Item has been added : {item.description}");
            if (item == null){ return; }

            Item itemTemp= new Item(item);

            InventoryObject inventory = GetInventoryObjectByType(InterfaceType.Inventory);

            if (inventory.AddItem(itemTemp, itemTemp.count))
            {
                Debug.Log("item Added");
            }
        }
        
        private void DropItemFromInventory(ItemObject item)
        {
            Debug.LogWarning($"Item has been added : {item.description}");
        }

        private void SellItemFromInventory(ItemObject item)
        {
            Debug.LogWarning($"Item has been added : {item.description}");
        }

        public InventoryObject GetInventoryObjectByType(InterfaceType interfaceType)
        {
            for (var i = 0; i < inventories.Length; i++)
            {
                if(inventories[i].interfaceType == interfaceType)
                {
                    return inventories[i];
                }
            }
            
            #if UNITY_EDITOR
            Debug.LogAssertion($"InventoryController doesn't contains Inventory Object with Type : {interfaceType}, Please set Inventory Object");
            #endif

            return null;
        }
    }
}