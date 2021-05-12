using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using RogueLike.Tooltip;

namespace RogueLike.Inventory
{
    public abstract class UserInterface : MonoBehaviour
    {
        public InterfaceType interfaceType;
        public InventoryObject inventory;
        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        [SerializeField] private InventoryManager inventoryController;

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            SetInventoryData();
            
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                inventory.GetSlots[i].parent = this;
                inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
            }

            CreateSlots();

            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        }

        private void SetInventoryData()
        {
            if(inventoryController == null)
            {
                inventoryController = FindObjectOfType<InventoryManager>();
            }

            inventory = inventoryController.GetInventoryObjectByType(interfaceType);
        }


        private void OnSlotUpdate(InventorySlot _slot)
        {
            if (_slot.item.Id >= 0)
            {
                _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
                _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.SlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            }
            else
            {
                _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.SlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.SlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        public abstract void CreateSlots();

        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoverOver = obj;
        }
        public void OnExit(GameObject obj)
        {
            MouseData.slotHoverOver = null;
        }
        public void OnEnterInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        }
        public void OnExitInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = null;
        }
        public void OnDragStart(GameObject obj)
        {
            MouseData.tempItemBeingDragged = CreateTampItem(obj);
        }


        public GameObject CreateTampItem(GameObject obj)
        {
            GameObject tempItem = null;

            if (slotsOnInterface[obj].item.Id >= 0)
            {
                tempItem = new GameObject();
                var rt = tempItem.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(50, 50);
                tempItem.transform.SetParent(transform.parent);
                var img = tempItem.AddComponent<Image>();
                img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
                img.raycastTarget = false;
            }

            return tempItem;
        }

        public void OnDragEnd(GameObject obj)
        {
            Destroy(MouseData.tempItemBeingDragged);

            if (MouseData.interfaceMouseIsOver == null)
            {
                slotsOnInterface[obj].RemoveItem();
                return;
            }

            if (MouseData.slotHoverOver)
            {
                InventorySlot mouseSlotHoverData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoverOver];
                inventory.SwapItems(slotsOnInterface[obj], mouseSlotHoverData);
            }
        }

        public void OnClick(GameObject obj)
        {
            var trigger = obj.GetComponent<TooltipTrigger>();
            
            trigger.content = slotsOnInterface[obj].amount.ToString();
        }

        public void OnDrag(GameObject obj)
        {
            if (MouseData.tempItemBeingDragged != null)
            {
                MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
            }
        }
    }
}