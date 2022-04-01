using UnityEngine;

namespace Services.GameInput
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
        protected override void HandleButtonClick(string buttonName)
        {
            OnAttackButton?.Invoke();
        }
    }
}