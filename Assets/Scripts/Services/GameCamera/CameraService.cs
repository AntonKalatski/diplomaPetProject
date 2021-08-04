using UnityEngine;

namespace Services.GameCamera
{
    public class CameraService : ICameraService
    {
        private Camera camera;
        public Camera GetCamera() => camera;
        public void SetFollower(Transform transform)
        {
            camera = ReferenceEquals(camera, Camera.main) ? camera : Camera.main;
            if (camera.transform.TryGetComponent<CameraFollower>(out var follower))
                follower.SetFollowTarget(transform);
        }
    }
}