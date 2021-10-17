using Factories.Interfaces;
using UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class OpenScreenButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private ScreenType screenType;
        private IScreenService screenService;

        public void Construct(IScreenService screenService)
        {
            this.screenService = screenService;
        }

        private void Awake() => button.onClick.AddListener(OpenScreenHandler);

        private void OpenScreenHandler() => screenService.Open(screenType);
    }
}