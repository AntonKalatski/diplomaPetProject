using Factories.Interfaces;
using GameSM.Interfaces;
using Services.GameProgress;

namespace GameSM.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IGameProgressService gameProgressService;
        private readonly IGameUIFactory gameUIFactory;
        
        public MainMenuState(
            GameStateMachine gameStateMachine,
            IGameProgressService gameProgressService,
            IGameUIFactory gameUIFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameProgressService = gameProgressService;
            this.gameUIFactory = gameUIFactory;
        }
        
        public void Enter()
        {
            gameUIFactory.CreateUIRoot();
            gameUIFactory.CreateMainMenu();
        }

        public void Exit()
        {
            
        }
    }
}