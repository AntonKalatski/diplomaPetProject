using System;
using Managers;
using Services.GameServiceLocator;
using UnityEngine;

namespace Services
{
    public interface IInputService : IService, ITickable
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
        event Action OnAttackButton;
        void AttackButtonPointerDown();
    }
}