using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RogueLike.Inventory
{
    public class StaticInterface : UserInterface
    {
        public GameObject[] slots;

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
            
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                GameObject slot = slots[i];

                AddEvent(slot, EventTriggerType.PointerEnter, delegate { OnEnter(slot); });
                AddEvent(slot, EventTriggerType.PointerExit, delegate { OnExit(slot); });
                AddEvent(slot, EventTriggerType.BeginDrag, delegate { OnDragStart(slot); });
                AddEvent(slot, EventTriggerType.EndDrag, delegate { OnDragEnd(slot); });
                AddEvent(slot, EventTriggerType.Drag, delegate { OnDrag(slot); });
                AddEvent(slot, EventTriggerType.PointerClick, delegate { OnClick(slot); });

                inventory.GetSlots[i].SlotDisplay = slot;

                slotsOnInterface.Add(slot, inventory.GetSlots[i]);
            }
        }
    }
}