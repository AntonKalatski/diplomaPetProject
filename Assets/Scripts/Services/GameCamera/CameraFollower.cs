using UnityEngine;

namespace Services.GameCamera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform following;
        [SerializeField] private float distance;
        [SerializeField] private float offsetY;

        public void SetFollowTarget(Transform transform) => following = transform;

        private void LateUpdate()
        {
            if (ReferenceEquals(following, null))
                return;
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;

            return followingPosition;
        }
    }
}