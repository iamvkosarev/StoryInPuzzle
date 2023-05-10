using System;
using System.Collections.Generic;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
    }

    public class GameStateMachine : IGameStateMachine
    {
        private readonly ServicesContainer _servicesContainer;
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;


        public GameStateMachine(ServicesContainer servicesContainer)
        {
            _servicesContainer = servicesContainer;
        }
        public void RegisterState<TState>() where TState : class, IState
        {
            var type = typeof(TState);
            if(_states.ContainsKey(type)) throw new InvalidOperationException($"State type of '{type}' was already registered");
            _states.Add(type, GetStateInstance<TState>());
        }

        private IState GetStateInstance<TState>()where TState : class, IState
        {
            return _servicesContainer.GetInstance<TState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            var newStateType = typeof(TState);
            if (!_states.ContainsKey(newStateType)) throw new InvalidOperationException("Selected state not presented in states list.");
            _currentState?.Exit();
            _currentState = _states[typeof(TState)];
            _currentState.Enter();
        }
    }
}