using UnityEngine;

namespace Services.Random
{
    public class RandomService : IRandomService
    {
        public bool CalculateChance(float min, float max, float chance)
        {
            var rnd = UnityEngine.Random.Range(min,max);
            return chance <= rnd;
        }
    }
}