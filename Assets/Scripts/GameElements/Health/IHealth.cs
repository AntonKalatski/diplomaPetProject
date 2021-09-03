using System;

namespace GameElements.Health
{
    public interface IHealth
    {
        float CurrentHealth { get; set; }
        float MaxHealth { get; set; }
        void AddOnHealthChangeListener(Action listener);
        void RemoveOnHealthChangeListener(Action listener);
        void TakeDamage(float damage);
    }
}