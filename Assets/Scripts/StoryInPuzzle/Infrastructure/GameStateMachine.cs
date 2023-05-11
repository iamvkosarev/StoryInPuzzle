using System;
using System.Collections.Generic;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload data) where TState : class, IPayloadState<TPayload>;
    }

    public class GameStateMachine : IGameStateMachine
    {
        private readonly ServicesContainer _servicesContainer;
        private readonly Dictionary<Type, IExitState> _states = new();
        private IExitState _currentState;


        public GameStateMachine(ServicesContainer servicesContainer)
        {
            _servicesContainer = servicesContainer;
        }
        public void RegisterState<TState>() where TState : class, IExitState
        {
            var type = typeof(TState);
            if(_states.ContainsKey(type)) throw new InvalidOperationException($"State type of '{type}' was already registered");
            _states.Add(type, GetStateInstance<TState>());
        }

        public void Enter<TState>() where TState : class, IState
        {
            ExitLastState();
            ((IState)SetState<TState>()).Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad data) where TState : class, IPayloadState<TPayLoad>
        {
            ExitLastState();
            ((IPayloadState<TPayLoad>)SetState<TState>()).Enter(data);
        }

        private IExitState SetState<TState>() where TState : class, IExitState
        {
            var state = GetState<TState>();
            _currentState = state;
            //Debug.Log($"<color=green>Enter</color>: {typeof(TState)}");
            return state;
        }

        private void ExitLastState()
        {
            /*if(_currentState != null)
                Debug.Log($"<color=red>Exit</color>: {_currentState.GetType()}");*/
            _currentState?.Exit();
        }

        private IExitState GetStateInstance<TState>()where TState : class, IExitState
        {
            return _servicesContainer.GetInstance<TState>();
        }

        private IExitState GetState<TState>() where TState : class, IExitState
        {
            var newStateType = typeof(TState);
            if (!_states.ContainsKey(newStateType))
                throw new InvalidOperationException("Selected state not presented in states list.");
            return _states[typeof(TState)];
        }
    }
}