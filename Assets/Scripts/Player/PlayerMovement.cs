using Constants;
using Extensions;
using GameData;
using Services;
using Services.GameCamera;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IProgressUpdatable
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField, Range(0, 10)] private float movementSpeed;

        private IInputService inputService;
        private Camera mainCam;

        private void Start()
        {
            inputService = ServiceLocator.Container.LocateService<IInputService>();
            mainCam = ServiceLocator.Container.LocateService<CameraService>().GetCamera();
        }

        private void Update()
        {
            Vector3 direction = Vector3.zero;

            if (inputService.Axis.sqrMagnitude > GameConstants.Epsilon)
            {
                direction = mainCam.transform.TransformDirection(inputService.Axis).normalized;
                direction.y = 0;
                transform.forward = direction;
            }

            direction += Physics.gravity;
            characterController.Move(direction * Time.deltaTime * movementSpeed);
        }

        public void UpdateProgress(PlayerProgressData progressData) =>
            progressData.WorldData.PositionOnLevel =
                new PositionOnLevel(GetSceneName(), transform.position.AsVectorPosition());

        public void LoadProgress(PlayerProgressData progressData)
        {
            if (ReferenceEquals(GetSceneName(), progressData.WorldData.PositionOnLevel.Level))
            {
                VectorPosition savedPos = progressData.WorldData.PositionOnLevel.Position;
                MoveToPosition(toPos: savedPos);
            }
        }

        private void MoveToPosition(VectorPosition toPos)
        {
            characterController.Move(toPos.AsUnityVector3());
            //characterController.enabled = false;
            // transform.position = savedPos.AsUnityVector3();
            //characterController.enabled = true;
        }

        private static string GetSceneName() => SceneManager.GetActiveScene().name;
    }
}