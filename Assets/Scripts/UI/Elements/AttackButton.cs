using System;
using Services;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class AttackButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private IInputService inputService;

        private void Start()
        {
            inputService = ServiceLocator.Container.LocateService<IInputService>();
        }

        public bool IsButtonUp { get; private set; } = false;
        public void OnPointerUp(PointerEventData eventData) => IsButtonUp = false;

        public void OnPointerDown(PointerEventData eventData) => inputService.AttackButtonPointerDown();
    }
}