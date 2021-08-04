using Bootstrap;
using GameSM.Interfaces;
using Services.GameServiceLocator;
using UI.Loading;

namespace GameSM
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ServiceLocator serviceLocator;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            ServiceLocator serviceLocator)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.serviceLocator = serviceLocator;
        }

        public void Enter()
        {
            curtain.Hide();
        }

        public void Exit()
        {
        }
    }
}