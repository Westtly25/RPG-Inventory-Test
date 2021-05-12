using UnityEngine;
using System;

namespace RogueLike.Inventory
{
    [Serializable]
    public class Socket
    {
        [SerializeField] private ScriptableSocketType socketType;
        [SerializeField] private GemsItem GemItem;
    }
}