using GameSM;
using GameSM.States;
using Services.GameProgress;
using UI.Elements;
using UnityEngine;

namespace UI.Screens.MainMenuScreen
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private LaunchGameButton newGameButton;

        private IGameStateMachine gameStateMachine;
        protected override void OnAwake() => closeButton.onClick.AddListener(Application.Quit);

        public void Construct(IGameStateMachine gameStateMachine, IGameProgressService progressService)
        {
            base.Construct(progressService);
            this.gameStateMachine = gameStateMachine;
        }

        protected override void Initialize() => newGameButton.SetButtonText(ProgressService.IsNewGame);
        protected override void SubscribeUpdates()
        {
            newGameButton.AddGameLaunchListener(GameLaunchHandler);
        }
        protected override void UnsubscribeUpdates() => newGameButton.RemoveGameLaunchListener(GameLaunchHandler);

        private void GameLaunchHandler()
        {
            Destroy(gameObject);
            gameStateMachine.Enter<LoadLevelState, string>(ProgressService.PlayerProgressData.worldData
                .PositionOnLevel
                .level);
        }
    }
}