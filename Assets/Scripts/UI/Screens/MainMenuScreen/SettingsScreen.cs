using Services.GameProgress;
using Services.GameSound;
using Services.GameVibration;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenuScreen
{
    public class SettingsScreen : BaseScreen
    {
        [SerializeField] private Button soundSettings;
        [SerializeField] private Button vibrationSettings;

        private ISoundService soundService;
        private IVibrationService vibrationService;

        public void Construct(IGameProgressService progressService, ISoundService soundService, IVibrationService vibrationService)
        {
            base.Construct(progressService);
            
        }

        protected override void Initialize()
        {
        }
        protected override void SubscribeUpdates()
        {
        }

        protected override void UnsubscribeUpdates()
        {
        }
    }
}