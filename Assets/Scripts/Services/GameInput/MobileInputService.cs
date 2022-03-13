using UnityEngine;

namespace Services.GameInput
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();

        public override bool IsAttackButtonUp()
        {
            Debug.LogError("From mobile inputService");
            return attackButton.IsButtonUp;
        }
    }
}