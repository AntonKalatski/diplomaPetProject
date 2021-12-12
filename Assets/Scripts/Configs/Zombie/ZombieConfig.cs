using Spawner;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs.Zombie
{
    [CreateAssetMenu(fileName = nameof(ZombieConfig), menuName = "Game/Zombies/" + nameof(ZombieConfig))]
    public class ZombieConfig : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject prefabReference;
        [SerializeField] private ZombieType zombieType;
        [SerializeField, Range(0, 5f)] private float attackCoolDown = 2f;
        [SerializeField, Range(0, 3f)] private float attackRadius = 1.5f;
        [SerializeField, Range(0, 3f)] private float attackHeight = 1.5f;
        [SerializeField, Range(5, 30f)] private float damage = 5f;
        [SerializeField, Range(1, 100)] private int hp = 25;
        public AssetReferenceGameObject PrefabReference => prefabReference;
        public ZombieType ZombieType => zombieType;
        public float AttackCoolDown => attackCoolDown;
        public float AttackRadius => attackRadius;
        public float AttackHeight => attackHeight;
        public float Damage => damage;
        public int Hp => hp;
    }
}