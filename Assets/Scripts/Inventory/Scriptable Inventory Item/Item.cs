using RogueLike.Characteristics;
using RogueLike.Crafting;
using RogueLike.CharactersType;

namespace RogueLike.Inventory
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public int count;
        public int Id = -1;
        public int currentUpgradeLevel;
        public int levelToEquip;
        public ScriptableTierType tier = null;
        public ScriptableRareType rareItemType = null;
        public ScriptableMaterialType itemMaterialType = null;
        public ScriptableCharacteristicsContainer itemsAttributes = null;
        public ScriptableCharacterType[] characters;
        public Socket[] sockets;

        public Item()
        {
            Name = "";
            Id = -1;
        }

        public Item(ItemObject item)
        {
            Name = item.name;
            count = item.count;
            Id = item.data.Id;
            currentUpgradeLevel = item.currentUpgradeLevel;
            levelToEquip = item.levelToEquip;
            tier = item.tierType;
            rareItemType = item.rareType;
            itemMaterialType = item.materialType;
            itemsAttributes = item.itemsAttributes;
            characters = item.characters;
            sockets = item.sockets;
        }
    }
}