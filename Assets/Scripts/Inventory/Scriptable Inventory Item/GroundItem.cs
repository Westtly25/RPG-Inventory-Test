using UnityEngine;
using RogueLike.EventsSO;

namespace RogueLike.Inventory
{
    public class GroundItem : MonoBehaviour
    {
        [Header("Event Inventory")]
        [SerializeField] private ItemEventSO itemEventSO;

        [Header("Inventory Item")]
        [SerializeField] public ItemObject item;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                itemEventSO.RisedEvent(item);
                Debug.Log($"Event Triggered - {item.description}");
            }
        }
    }
}