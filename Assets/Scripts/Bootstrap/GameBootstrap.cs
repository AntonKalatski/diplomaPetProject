using GameSM.States;
using Managers;
using UI.Loading;
using UnityEngine;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        //[SerializeField] private AssetReference loadingScreen;
        [SerializeField] private LoadingCurtain loadingScreenPrefab;
        private Game game;
        private TickableManager tickableManager;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            tickableManager = new GameObject("Tickable Manager").AddComponent<TickableManager>();
            game = new Game(this, Instantiate(loadingScreenPrefab), tickableManager);
            Game.GameStateMachine.Enter<BootstrapState>();
        }
    }
}