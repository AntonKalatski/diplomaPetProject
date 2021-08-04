using Services.GameServiceLocator;
using UnityEngine;

namespace Services.GameCamera
{
    public interface ICameraService : IService
    {
        Camera GetCamera();
        void SetFollower(Transform transform);
    }
}