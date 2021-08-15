using GameData;
using Services.GameServiceLocator;

namespace Services.GameProgress
{
    public interface IGameProgressService : IService
    {
        public bool IsNewGame { get; set; }
        PlayerProgressData PlayerProgressData { get; set; }
    }
}