using Bootstrap;
using Services;
using Services.GameCamera;
using Services.GameInput;
using UnityEngine;

namespace GameState
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            RegisterServices();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
            Game.CameraService = RegisterCameraService();
        }

        private IInputService RegisterInputService()
        {
#if UNITY_EDITOR
            return new MobileInputService();
#else
            return new StandaloneInputService();
#endif
        }

        private ICameraService RegisterCameraService() => new CameraService(Camera.main);
    }
}