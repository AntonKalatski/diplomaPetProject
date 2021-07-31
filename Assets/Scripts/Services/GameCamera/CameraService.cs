using UnityEngine;

namespace Services.GameCamera
{
    public class CameraService : ICameraService
    {
        private Camera camera;
        private CameraFollower cameraFollower;

        public CameraService(Camera camera)
        {
            this.camera = camera;
            if (camera.transform.TryGetComponent<CameraFollower>(out var follower))
                cameraFollower = follower;
        }


        public void SetFollow(Transform transform)
        {
            cameraFollower.Follow(transform);
        }

        public void SetCamera(Camera camera)
        {
            if (!ReferenceEquals(camera, null))
                this.camera = camera;
        }

        public Camera GetCamera() => camera;
    }
}