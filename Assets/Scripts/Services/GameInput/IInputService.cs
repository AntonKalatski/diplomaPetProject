using System;
using Services.GameServiceLocator;
using UnityEngine;

namespace Services
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
        event Action OnAttackButton;
        void AttackButtonPointerDown();
    }
}