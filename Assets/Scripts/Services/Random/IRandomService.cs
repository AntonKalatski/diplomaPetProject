using Services.GameServiceLocator;

namespace Services.Random
{
    public interface IRandomService : IService
    {
        bool CalculateChance(float min, float max, float chanse);
    }
}