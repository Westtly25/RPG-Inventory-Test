using UnityEngine;

namespace RogueLike.Inventory
{
    [CreateAssetMenu(fileName = "Items Database Object", menuName = "Rogue Like Prototype/Inventory/New Items Database Object", order = 0)]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] ItemObjects;

        [ContextMenu("Update ID's")]
        public void UpdateID()
        {
            for (int i = 0; i < ItemObjects.Length; i++)
            {
                if (ItemObjects[i].data.Id != i)
                {
                    ItemObjects[i].data.Id = i;
                }
            }
        }

        public void OnAfterDeserialize()
        {
            UpdateID();
        }

        public void OnBeforeSerialize()
        {
        }
    }
}