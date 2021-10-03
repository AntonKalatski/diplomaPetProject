using System.Collections.Generic;
using Configs.LootConfig;

namespace GameData
{
    [System.Serializable]
    public class LootData
    {
        public Dictionary<LootType, int> playersLoot = new Dictionary<LootType, int>();

        public void CollectLootItem(LootItem item)
        {
            if (!playersLoot.ContainsKey(item.type))
                playersLoot.Add(item.type, item.value);
            else
                playersLoot[item.type] += item.value;
        }

        public void RemoveLootItem(LootType type, int value)
        {
            if (!playersLoot.ContainsKey(type)) return;
            if (playersLoot[type] > 0)
                playersLoot[type]--;
        }
    }
}