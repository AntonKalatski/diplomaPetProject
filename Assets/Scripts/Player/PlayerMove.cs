using Bootstrap;
using Constants;
using Services;
using Services.GameCamera;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField, Range(0, 10)] private float movementSpeed;
        [SerializeField, Range(0, 0.5f)] private float smoothTime;

        private IInputService inputService;
        private ICameraService cameraService;
        private Camera _camera;
        private Vector3 velocity;

        private void Awake()
        {
            inputService = Game.InputService;
            cameraService = Game.CameraService;
        }

        private void Start()
        {
            cameraService.SetFollow(transform);
            _camera = cameraService.GetCamera();
        }

        private void Update()
        {
            Vector3 direction =
                _camera.transform.TransformDirection(inputService.Axis.normalized);
            if (inputService.Axis.sqrMagnitude > GameConstants.Epsilon)
            {
                direction.y = 0;
                // transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref velocity, smoothTime);
                transform.forward = direction;
            }
                
            direction += Physics.gravity;
            characterController.Move(direction * Time.deltaTime * movementSpeed);
        }
    }
}
