using GameSM.States;
using UI.Loading;
using UnityEngine;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingScreenPrefab;
        private Game game;

        private void Awake()
        {
            game = new Game(this, Instantiate(loadingScreenPrefab));
            Game.GameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}