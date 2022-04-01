using UnityEngine;

namespace Services.GameInput
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();
                if (axis == Vector2.zero)
                    axis = UnityAxis();
                return axis;
            }
        }

        protected override void HandleButtonClick(string buttonName)
        {
            Debug.Log($"Button click {buttonName}");
        }

        public override void Tick()
        {
            if (Input.GetButtonDown("Fire1"))
                OnAttackButton?.Invoke();
        }

        private static Vector2 UnityAxis() => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}