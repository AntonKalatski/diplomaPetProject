using Configs;
using Configs.Container;
using Configs.LootConfig;
using Configs.Screens;
using Configs.Zombie;
using Services.GameServiceLocator;
using Spawner;
using UI.Services;

namespace Services.Configs.Zombie
{
    public interface IConfigsService : IService
    {
        void LoadConfigs();
        LevelData ForLevel(string sceneKey);
        LootConfig ForLoot(LootType type);
        ZombieConfig ForZombie(ZombieType type);
        TConfig ForConfig<TConfig>(ConfigType playerConfig) where TConfig : AbstractGameConfig;
        ScreenConfig ForScreen(ScreenType screenType);
    }
}