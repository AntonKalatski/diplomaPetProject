using System;
using System.Collections.Generic;

namespace GameData
{
    [Serializable]
    public class KillData
    {
        public List<string> clearedSpawners = new List<string>();
        public int killedZombies;
        private Action onZombieKilled;
        public void AddSkullCount(int amount)
        {
            killedZombies+=amount;
            onZombieKilled?.Invoke();
        }

        public void AddKillCounterListener(Action listener) => onZombieKilled += listener;
        public void RemoveKillCounterListener(Action listener) => onZombieKilled -= listener;
    }
}