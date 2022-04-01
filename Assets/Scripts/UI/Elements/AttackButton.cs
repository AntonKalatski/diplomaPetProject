using System;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class AttackButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IInputProvider
    {
        [SerializeField] private string buttonName = "Fire1";
        public Action<string> OnButtonClickProvide { get; set; }

        public void OnPointerUp(PointerEventData eventData) 
        {
        }
        public void OnPointerDown(PointerEventData eventData) => OnButtonClickProvide?.Invoke(buttonName);
    }
}