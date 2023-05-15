using Core.Common;
using StoryInPuzzle.FiddingObjects;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.Curtain;
using StoryInPuzzle.Infrastructure.Services.LevelContext;
using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;
using StoryInPuzzle.PlayerMovement;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public sealed class LoadLevelState : IPayloadState<int>
    {
        private readonly IGameConfig _gameConfig;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _curtain;
        private readonly IGameStateMachine _stateMachine;
        private readonly ILevelProgress _levelProgress;
        private readonly ILevelContext _levelContext;
        private readonly IPlayerInput _playerInput;

        public LoadLevelState(IGameConfig gameConfig, ISceneLoader sceneLoader,
            ICurtain curtain, IGameStateMachine stateMachine, ILevelProgress levelProgress, ILevelContext levelContext, IPlayerInput playerInput
        )
        {
            _gameConfig = gameConfig;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _stateMachine = stateMachine;
            _levelProgress = levelProgress;
            _levelContext = levelContext;
            _playerInput = playerInput;
        }

        public async void Enter(int data)
        {
            _curtain.Show();
            await _sceneLoader.LoadScene(_gameConfig.LevelsConfig.Levels[data]);
            _curtain.Hide();
            SetLevelProgress();
            SetLevelContext(data);
            SetUpScene();
            _stateMachine.Enter<GameLoopState>();
        }

        private void SetUpScene()
        {
            var monoBehaviours = GameObject.FindObjectsOfType<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                if(monoBehaviour.TryGetComponent(out IPlayerInputUser playerInputUser))
                    playerInputUser.Init(_playerInput);
                if(monoBehaviour.TryGetComponent(out IObjectHunter objectHunter))
                    objectHunter.SetLevelProgress(_levelProgress);
            }
        }

        private void SetLevelContext(int levelIndex)
        {
            _levelContext.LevelIndex = levelIndex;
            _levelContext.PlayerComponent = GameObject.FindObjectOfType<PlayerComponent>();
        }

        private void SetLevelProgress()
        {
            _levelProgress.ClearLevelProgress();
            var hiddenObjects = GameObject.FindObjectsOfType<HiddenObject>();
            foreach (var hiddenObject in hiddenObjects)
            {
                _levelProgress.AddHiddenObject(hiddenObject);
            }
        }

        public void Exit()
        {
        }
    }
}