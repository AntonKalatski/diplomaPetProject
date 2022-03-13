using UI.Elements;
using UnityEngine;

namespace Services.GameInput
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Button = "Fire1";
        protected AttackButton attackButton;
        public abstract Vector2 Axis { get; }

        public abstract bool IsAttackButtonUp();

        public void RegisterAttackButton(AttackButton button)
        {
            attackButton = button;
        }
        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}