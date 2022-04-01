using System;
using UnityEngine;

namespace Services.GameInput
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Button = "Fire";
        protected Action OnAttackButton;
        public abstract Vector2 Axis { get; }


        public void AddInputListener(IInputListener inputListener)
        {
            OnAttackButton += inputListener.AttackButton;
        }

        public void RemoveInputListener(IInputListener inputListener)
        {
            OnAttackButton -= inputListener.AttackButton;
        }

        public void AddInputPorvider(IInputProvider inputProvider)
        {
            inputProvider.OnButtonClickProvide += HandleButtonClick;
        }

        public void RemoveInputPorvider(IInputProvider inputProvider)
        {
            inputProvider.OnButtonClickProvide -= HandleButtonClick;
        }

        protected abstract void HandleButtonClick(string buttonName);

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        public virtual void Tick()
        {
        }

        // public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(Button);
    }
}