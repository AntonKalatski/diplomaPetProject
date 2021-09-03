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
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, IProgressSaveable
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Rigidbody rb;
        [SerializeField, Range(0, 10)] private float movementSpeed;

        private IInputService inputService;
        private Camera mainCam;

        private void Awake() => characterController ??= GetComponent<CharacterController>();

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

        public void SaveProgress(PlayerProgressData progressData) =>
            progressData.worldData.PositionOnLevel =
                new PositionOnLevel(GetSceneName(), transform.position.AsVectorPosition(),
                    transform.rotation.eulerAngles.AsVectorPosition());

        public void LoadProgress(PlayerProgressData progressData)
        {
            Debug.Log("Load progress in player movement");
            if (ReferenceEquals(GetSceneName(), progressData.worldData.PositionOnLevel.level)) return;
            VectorStruct savedPos = progressData.worldData.PositionOnLevel.position;
            VectorStruct savedRot = progressData.worldData.PositionOnLevel.rotation;
            Warp(toPos: savedPos, toRot: savedRot);
        }

        private void Warp(VectorStruct toPos, VectorStruct toRot)
        {
            characterController.enabled = false;
            rb.isKinematic = true;
            transform.position = toPos.AsUnityVector3().AddY(characterController.height);
            var rot = transform.rotation;
            rot.eulerAngles = toRot.AsUnityVector3();
            transform.rotation = rot;
            characterController.enabled = true;
            rb.isKinematic = false;
        }

        private static string GetSceneName() => SceneManager.GetActiveScene().name;
    }
}