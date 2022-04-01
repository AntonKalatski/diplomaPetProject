using Managers;
using Services.GameServiceLocator;
using UnityEngine;

namespace Services
{
    public interface IInputService : IService, ITickable
    {
        Vector2 Axis { get; }
        void AddInputListener(IInputListener inputListener);
        void RemoveInputListener(IInputListener inputListener);
        void AddInputPorvider(IInputProvider inputProvider);
        void RemoveInputPorvider(IInputProvider inputProvider);
    }
}