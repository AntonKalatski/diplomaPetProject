using System;
using System.Collections.Generic;

namespace GameState
{
    public class GameStateMachine
    {
        private Dictionary<Type,IState> states;
        private IState activeState;

        public GameStateMachine()
        {
            states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
            };
        }
        
        public void Enter<TState>() where TState : IState
        {
            activeState?.Exit();
            activeState = states[typeof(TState)];
            activeState.Enter();
        }
    }
}