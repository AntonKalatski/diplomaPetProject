using UnityEngine;

namespace Zombies
{
    public class ZombieBodyProvider : MonoBehaviour
    {
        [SerializeField] private Transform hips;

        public Transform Hips => hips;
    }
}