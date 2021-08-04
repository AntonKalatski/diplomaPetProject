using GameData;
using Services.GameServiceLocator;

namespace Services.GameProgress
{
    public interface IGameProgressService : IService
    {
        PlayerProgressData PlayerProgressData { get; set; }
    }
}