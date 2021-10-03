using System.Collections.Generic;
using Extensions;
using Factories.Interfaces;
using GameData;
using Services.GameProgress;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "progress";
        private readonly IGameProgressService gameProgressService;
        private readonly IGamePrefabFactory gamePrefabFactory;
        private readonly IGameUIFactory gameUIFactory;

        public SaveLoadService(IGameProgressService gameProgressService,
            IGamePrefabFactory gamePrefabFactory,
            IGameUIFactory gameUIFactory)
        {
            this.gameProgressService = gameProgressService;
            this.gamePrefabFactory = gamePrefabFactory;
            this.gameUIFactory = gameUIFactory;
        }

        public void SaveProgress()
        {
            gamePrefabFactory.ProgressSaveables.ForEach(x => x.SaveProgress(gameProgressService.PlayerProgressData));
            gameUIFactory.ProgressSaveables.ForEach(x => x.SaveProgress(gameProgressService.PlayerProgressData));
            PlayerPrefs.SetString(ProgressKey, gameProgressService.PlayerProgressData.ToJson());
        }

        public PlayerProgressData LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgressData>();
    }
}