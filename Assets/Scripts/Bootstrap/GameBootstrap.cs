using Configs.Container;
using GameSM.States;
using UI.Loading;
using UnityEngine;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingCurtain;
        [SerializeField] private GameConfigsContainer configsContainer;
        private Game game;
        private void Awake()
        {
            game = new Game(this,configsContainer,loadingCurtain);
            Game.GameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}