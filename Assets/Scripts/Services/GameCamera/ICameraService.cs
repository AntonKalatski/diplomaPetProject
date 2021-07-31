using UnityEngine;

namespace Services.GameCamera
{
    public interface ICameraService
    {
        public void SetFollow(Transform transform);
        public void SetCamera(Camera camera);
        public Camera GetCamera();
    }
}