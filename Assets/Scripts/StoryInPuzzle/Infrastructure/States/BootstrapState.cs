using StoryInPuzzle.Infrastructure.Services.Data;

namespace StoryInPuzzle.Infrastructure.States
{
    public sealed class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveLoadData _saveLoadData;

        public BootstrapState(IGameStateMachine gameStateMachine, ISaveLoadData saveLoadData)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadData = saveLoadData;
        }

        public void Enter()
        {
            _saveLoadData.Load();
            _gameStateMachine.Enter<LoginState>();
        }

        public void Exit()
        {
        }
    }
}