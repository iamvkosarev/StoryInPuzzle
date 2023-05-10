using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.LoadingScreen;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;

namespace StoryInPuzzle.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<int>
    {
        private readonly IGameConfigProvider _configProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _curtain;
        private readonly IGameStateMachine _stateMachine;

        public LoadLevelState(IGameConfigProvider configProvider, ISceneLoader sceneLoader,
            ICurtain curtain, IGameStateMachine stateMachine
        )
        {
            _configProvider = configProvider;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _stateMachine = stateMachine;
        }

        public async void Enter(int levelIndex)
        {
            _curtain.Show();
            await _sceneLoader.LoadScene(_configProvider.GameConfig.LevelsConfig.Levels[levelIndex]);
            _curtain.Hide();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}