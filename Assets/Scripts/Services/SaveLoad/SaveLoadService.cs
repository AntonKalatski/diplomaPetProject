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
        private List<IGameFactory> factories;

        public SaveLoadService(IGameProgressService gameProgressService,
            IGamePrefabFactory gamePrefabFactory,
            IGameUIFactory gameUIFactory)
        {
            InitializeGameFactories(gamePrefabFactory, gameUIFactory);
            this.gameProgressService = gameProgressService;
        }

        private void InitializeGameFactories(IGamePrefabFactory prefabFactory, IGameUIFactory uiFactory)
        {
            factories = new List<IGameFactory>();
            factories.Add(prefabFactory);
            factories.Add(uiFactory);
        }

        public void SaveProgress()
        {
            foreach (IGameFactory factory in factories)
                factory.ProgressSaveables.ForEach(x => x.SaveProgress(gameProgressService.PlayerProgressData));
            PlayerPrefs.SetString(ProgressKey, gameProgressService.PlayerProgressData.ToJson());
        }

        public PlayerProgressData LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgressData>();
    }
}