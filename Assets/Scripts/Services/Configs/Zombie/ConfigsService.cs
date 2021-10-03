using System.Collections.Generic;
using System.Linq;
using Configs;
using Configs.Container;
using Configs.LootConfig;
using Configs.Zombie;
using Spawner;
using UnityEngine;

namespace Services.Configs.Zombie
{
    public class ConfigsService : IConfigsService
    {
        private Dictionary<string, LevelData> levels;
        private Dictionary<ZombieType, ZombieConfig> zombies;
        private Dictionary<LootType, LootConfig> loot;
        private Dictionary<ConfigType, AbstractGameConfig> gameConfigs;

        public void LoadConfigs()
        {
            gameConfigs = Resources
                .LoadAll<AbstractGameConfig>("Configs/Game")
                .ToDictionary(x => x.Type, x => x);
            levels = Resources
                .LoadAll<LevelData>("Configs/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
            loot = Resources
                .LoadAll<LootConfig>("Configs/Loot")
                .ToDictionary(x => x.LootType, x => x);
            zombies = Resources
                .LoadAll<ZombieConfig>("Configs/Zombies")
                .ToDictionary(x => x.ZombieType, x => x);
        }

        public ZombieConfig ForZombie(ZombieType type) =>
            zombies.TryGetValue(type, out ZombieConfig zombieConfig)
                ? zombieConfig
                : null;

        public TConfig ForConfig<TConfig>(ConfigType playerConfig) where TConfig : AbstractGameConfig
        {
            if (gameConfigs.TryGetValue(playerConfig, out var gameConfig))
                return gameConfig as TConfig;
            return null;
        }

        public LootConfig ForLoot(LootType type) =>
            loot.TryGetValue(type, out LootConfig zombieConfig)
                ? zombieConfig
                : null;

        public LevelData ForLevel(string sceneKey) =>
            levels.TryGetValue(sceneKey, out LevelData levelData)
                ? levelData
                : null;
    }
}