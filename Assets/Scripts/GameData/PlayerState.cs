using System;

namespace GameData
{
    [Serializable]
    public class PlayerState
    {
        public float currentHp;
        public float maxHp;

        public PlayerState()
        {
        }

        public void ResetHp() => currentHp = maxHp;
    }
}