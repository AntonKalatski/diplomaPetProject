using System;
using Services;
using Services.GameInput;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class AttackButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private IInputService inputService;
        public bool IsButtonUp { get; private set; }
        private void Awake()
        {
            inputService = ServiceLocator.Container.LocateService<IInputService>();
            inputService.RegisterAttackButton(this);
        }
        public void OnPointerUp(PointerEventData eventData) => IsButtonUp = true;

        public void OnPointerDown(PointerEventData eventData) => IsButtonUp = false;
    }
}