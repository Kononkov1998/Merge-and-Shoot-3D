using System;
using System.Collections.Generic;

namespace Core.StateMachine
{
    public abstract class BaseStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        private IExitState _activeState;

        protected BaseStateMachine()
        {
            _states = new Dictionary<Type, IExitState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        protected void RegisterState<TState>(TState state) where TState : class, IExitState =>
            _states.Add(typeof(TState), state);

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();

            var newState = GetState<TState>();
            _activeState = newState;

            return newState;
        }

        private TState GetState<TState>() where TState : class, IExitState =>
            _states[typeof(TState)] as TState;
    }
}