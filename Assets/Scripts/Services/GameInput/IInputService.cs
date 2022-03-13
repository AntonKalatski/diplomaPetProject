using Services.GameServiceLocator;
using UI.Elements;
using UnityEngine;

namespace Services
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
        void RegisterAttackButton(AttackButton attackButton);
    }
}