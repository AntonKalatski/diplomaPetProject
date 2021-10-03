using UnityEngine;

namespace Services.Random
{
    public class RandomService : IRandomService
    {
        public bool CalculateChance(float min, float max, float chance)
        {
            var rnd = UnityEngine.Random.Range(min,max); 
            Debug.Log($"Random service rnd = {rnd}");
            return chance <= rnd;
        }
    }
}