using GameSM.Interfaces;
using Services.GameServiceLocator;

namespace GameSM
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        TState ChangeState<TState>() where TState : class, IExitableState;
        TState GetState<TState>() where TState : class, IExitableState;
    }
}