using UnityEngine;

namespace Player
{
    public class PlayerBodyProvider : MonoBehaviour
    {
        [SerializeField] private Transform hips;
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        public Transform LeftHand => leftHand;

        public Transform RightHand => rightHand;
        public Transform Hips => hips;
    }
}