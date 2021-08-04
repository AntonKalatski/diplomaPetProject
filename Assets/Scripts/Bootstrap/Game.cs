using Configs.Container;
using GameSM;
using Services.GameServiceLocator;
using UI.Loading;

namespace Bootstrap
{
    public class Game
    {
        public static GameConfigsContainer GameConfigs;
        public static GameStateMachine GameStateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, GameConfigsContainer configsContainer,
            LoadingCurtain curtain)
        {
            GameConfigs = configsContainer;
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),curtain, ServiceLocator.Container);
        }
    }
}