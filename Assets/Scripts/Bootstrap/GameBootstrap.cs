using GameSM.States;
using UI.Loading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        //[SerializeField] private AssetReference loadingScreen;
        [SerializeField] private LoadingCurtain loadingScreenPrefab;
        private Game game;

        private void Awake()
        {
            //todo 
            game = new Game(this, Instantiate(loadingScreenPrefab));
            Game.GameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}