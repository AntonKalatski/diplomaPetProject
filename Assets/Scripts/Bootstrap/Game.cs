using Configs.Container;
using GameSM;
using Managers;
using Services.GameServiceLocator;
using UI.Loading;

namespace Bootstrap
{
    public class Game
    {
        public static GameStateMachine GameStateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, TickableManager tickableManager)
        {
            GameStateMachine =
                new GameStateMachine(new SceneLoader(coroutineRunner), curtain, ServiceLocator.Container,tickableManager);
        }
    }
}