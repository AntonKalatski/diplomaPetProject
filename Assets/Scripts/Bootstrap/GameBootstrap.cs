using Configs.Container;
using GameSM.States;
using UI.Loading;
using UnityEngine;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingScreenPrefab;
        [SerializeField] private GameConfigsContainer configsContainer;
        private Game game;

        private void Awake()
        {
            game = new Game(this, configsContainer, Instantiate(loadingScreenPrefab));
            Game.GameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}