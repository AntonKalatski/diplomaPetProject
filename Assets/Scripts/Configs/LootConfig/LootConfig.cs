using UnityEngine;

namespace Configs.LootConfig
{
    [CreateAssetMenu(fileName = nameof(LootConfig), menuName = "Game/Loot/" + nameof(LootConfig))]
    public class LootConfig : ScriptableObject
    {
        [SerializeField] private LootType lootType;
        [SerializeField] private GameObject lootObject;

        public LootType LootType => lootType;
        public GameObject LootObject => lootObject;
    }
}