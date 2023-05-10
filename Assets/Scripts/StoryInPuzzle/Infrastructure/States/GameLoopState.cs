using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private const string StartSceneKey = "Start";
        private readonly IPlayerGameScreenProvider _playerGameScreenProvider;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private PlayerGameScreen _screen;

        public GameLoopState(IPlayerGameScreenProvider playerGameScreenProvider, IGameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _playerGameScreenProvider = playerGameScreenProvider;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            _screen = await _playerGameScreenProvider.Load();
            _screen.OpenMenuButton.onClick.AddListener(OpenMenu);
            _screen.OpenTaskButton.onClick.AddListener(OpenTask);
        }

        public void Exit()
        {
            _screen.OpenMenuButton.onClick.RemoveListener(OpenMenu);
            _screen.OpenTaskButton.onClick.RemoveListener(OpenTask);
            _playerGameScreenProvider.Unload();
        }

        private void OpenTask()
        {
            Debug.Log("Open task");
        }

        private async void OpenMenu()
        {
            await _sceneLoader.LoadScene(StartSceneKey);
            _stateMachine.Enter<SelectLevelsState>();
        }
    }
}