using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RogueLike.Inventory
{
    public class DynamicInterface : UserInterface
    {
        public GameObject grid;
        public GameObject inventoryPrefab;

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                GameObject objSlot = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, grid.transform);

                AddEvent(objSlot, EventTriggerType.PointerEnter, delegate { OnEnter(objSlot); });
                AddEvent(objSlot, EventTriggerType.PointerExit, delegate { OnExit(objSlot); });
                AddEvent(objSlot, EventTriggerType.BeginDrag, delegate { OnDragStart(objSlot); });
                AddEvent(objSlot, EventTriggerType.EndDrag, delegate { OnDragEnd(objSlot); });
                AddEvent(objSlot, EventTriggerType.Drag, delegate { OnDrag(objSlot); });
                AddEvent(objSlot, EventTriggerType.PointerClick, delegate { OnClick(objSlot); });

                inventory.GetSlots[i].SlotDisplay = objSlot;

                slotsOnInterface.Add(objSlot, inventory.GetSlots[i]);
            }
        }
    }
}