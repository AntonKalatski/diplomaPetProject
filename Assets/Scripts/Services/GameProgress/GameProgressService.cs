using GameData;

namespace Services.GameProgress
{
    public class GameProgressService : IGameProgressService
    {
        public bool IsNewGame { get; set; }
        public PlayerProgressData PlayerProgressData { get; set; }
    }
}