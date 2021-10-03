using Configs.Container;
using GameSM;
using Services.GameServiceLocator;
using UI.Loading;

namespace Bootstrap
{
    public class Game
    {
        public static GameStateMachine GameStateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            GameStateMachine =
                new GameStateMachine(new SceneLoader(coroutineRunner), curtain, ServiceLocator.Container);
        }
    }
}