using System;
using UI.Elements;
using UnityEngine;

namespace Services.GameInput
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Button = "Fire";
        protected AttackButton attackButton;
        public abstract Vector2 Axis { get; }
        public event Action OnAttackButton;
        public void AttackButtonPointerDown()
        {
            Debug.LogError("AttackButton CLick!");
            OnAttackButton?.Invoke();
        }

        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(Button);

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}