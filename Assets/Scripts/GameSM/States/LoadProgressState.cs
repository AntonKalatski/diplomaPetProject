using Constants;
using GameData;
using GameSM.Interfaces;
using Services.GameProgress;
using Services.SaveLoad;
using UnityEngine;

namespace GameSM.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IGameProgressService gameProgressService;
        private readonly ISaveLoadService saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,
            IGameProgressService gameProgressService,
            ISaveLoadService saveLoadService)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameProgressService = gameProgressService;
            this.saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            TryLoadProgress();
            gameStateMachine.Enter<LoadLevelState, string>(gameProgressService.PlayerProgressData.worldData
                .PositionOnLevel
                .level);
        }

        public void Exit()
        {
        }

        private void TryLoadProgress() => gameProgressService.PlayerProgressData = saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgressData NewProgress()
        {
            gameProgressService.IsNewGame = true;
            var progress = new PlayerProgressData(GameConstants.City);
            progress.playerState.maxHp = 100;
            progress.playerState.ResetHp();
            progress.playerStats.Damage = 50f;
            progress.playerStats.DamageRadius = 2.5f;
            return progress;
        }
    }
}