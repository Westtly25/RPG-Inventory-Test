using UnityEngine;
using RogueLike.CharactersType;
using RogueLike.Crafting;
using RogueLike.Characteristics;

namespace RogueLike.Inventory
{
    public class ItemObject : ScriptableObject
    {
        [Header("Base Attributes")]
        [TextArea(1, 3)]
        [SerializeField] public string title;

        [TextArea(5, 10)]
        [SerializeField] public string description;
        [SerializeField] public int count = 1;

        [SerializeField] public Sprite uiDisplay;

        [Header("Boolian Parameters")]
        [SerializeField] public bool stackable;
        [SerializeField] public bool upgradable;

        [Header("Item Levels")]
        [SerializeField] public int currentUpgradeLevel;
        [Range(1, 5)]
        [SerializeField] public int maxUpgradeLevel;
        [SerializeField] public int levelToEquip;

        [Header("Item Type")]
        [SerializeField] public ScriptableItemType itemType;
        [SerializeField] public ScriptableTierType tierType;
        [SerializeField] public ScriptableRareType rareType;
        [SerializeField] public ScriptableMaterialType materialType;
        [SerializeField] public ScriptableCharacterType[] characters;
        [SerializeField] public Socket[] sockets;

        [SerializeField] public GameObject objectToDisplay;

        [Header("Attributes list")]
        [Space] [SerializeField] public ScriptableCharacteristicsContainer itemsAttributes;

        //[Header("Enchants list")]
        //[SerializeField] public Enchants[] enchants;

        public Item data = new Item();

        public Item CreateItem()
        {
            Item newItem = new Item(this);
            return newItem;
        }
    }
}