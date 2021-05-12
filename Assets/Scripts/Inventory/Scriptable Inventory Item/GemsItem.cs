using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueLike.Inventory
{
    [CreateAssetMenu(fileName = "Gems Item", menuName = "Rogue Like Prototype/Inventory/Items/New Gems Item", order = 0)]
    public class GemsItem : ItemObject
    {
        [SerializeField] private ScriptableSocketType socketType;
    }
}