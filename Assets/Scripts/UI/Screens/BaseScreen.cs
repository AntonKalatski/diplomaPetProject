using GameData;
using Services.GameProgress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] protected Button closeButton;
        private IGameProgressService progressService;
        protected PlayerProgressData Progress => progressService.PlayerProgressData;

        protected virtual void Construct(IGameProgressService progressService)
        {
            this.progressService = progressService;
        }

        private void Awake() => OnAwake();

        protected virtual void OnAwake() => closeButton.onClick.AddListener(() => Destroy(gameObject));

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => UnsubscribeUpdates();

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void UnsubscribeUpdates()
        {
        }
    }
}