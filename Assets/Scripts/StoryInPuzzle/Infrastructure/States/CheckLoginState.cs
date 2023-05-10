using System.Threading.Tasks;
using Sirenix.Utilities;
using StoryInPuzzle.Infrastructure.Services.Data;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public class CheckLoginState : IState
    {
        private const string LoginScene = "Login Scene";
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameDataContainer _gameDataContainer;

        public CheckLoginState(IGameStateMachine stateMachine, IGameDataContainer gameDataContainer)
        {
            _stateMachine = stateMachine;
            _gameDataContainer = gameDataContainer;
        }
        public async void Enter()
        {
            if (IsPlayerSelectedNickName())
            {
                _stateMachine.Enter<SelectLevelsState>();
            }
            else
            {
                _stateMachine.Enter<LoginState>();
            }
            
        }

        private bool IsPlayerSelectedNickName() => !_gameDataContainer.GameData.PlayerData.NickName.IsNullOrWhitespace();

        public void Exit()
        {
        }
    }
}