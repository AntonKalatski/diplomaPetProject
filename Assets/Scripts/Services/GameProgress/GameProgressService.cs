using GameData;

namespace Services.GameProgress
{
    public class GameProgressService : IGameProgressService
    {
        public PlayerProgressData PlayerProgressData { get; set; }
    }
}