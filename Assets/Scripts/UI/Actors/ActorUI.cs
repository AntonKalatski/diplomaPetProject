using GameElements.Health;
using UI.Bars;
using UnityEngine;

namespace UI.Actors
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;
        private IHealth health;
        public void Initialize(IHealth playerHealth)
        {
            health = playerHealth;
            health.AddOnHealthChangeListener(UpdateHpBar);
        }

        private void UpdateHpBar() => hpBar.SetValue(health.CurrentHealth, health.MaxHealth);
        private void OnDestroy() => health.RemoveOnHealthChangeListener(UpdateHpBar);
    }
}