using Configs;
using Configs.Container;
using Configs.LootConfig;
using Configs.Zombie;
using Services.GameServiceLocator;
using Spawner;

namespace Services.Configs.Zombie
{
    public interface IConfigsService : IService
    {
        LevelData ForLevel(string sceneKey);
        void LoadConfigs();
        LootConfig ForLoot(LootType type);
        ZombieConfig ForZombie(ZombieType type);
        TConfig ForConfig<TConfig>(ConfigType playerConfig) where TConfig : AbstractGameConfig;
    }
}