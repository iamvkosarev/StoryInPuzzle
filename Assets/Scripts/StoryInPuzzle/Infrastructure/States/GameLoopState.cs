using System;
using System.Collections;
using Heatmap.Scripts.Recorder;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen;
using StoryInPuzzle.Infrastructure.Services.Curtain;
using StoryInPuzzle.Infrastructure.Services.Data;
using StoryInPuzzle.Infrastructure.Services.LevelContext;
using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoryInPuzzle.Infrastructure.States
{
    public sealed class GameLoopState : IState
    {
        private const string StartSceneKey = "Start";
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPlayerGameScreenProvider _playerGameScreenProvider;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPlayerInput _playerInput;
        private readonly ILevelProgress _levelProgress;
        private readonly ICurtain _curtain;
        private readonly ILevelContext _levelContext;
        private readonly IGameDataContainer _gameDataContainer;
        private readonly ISaveLoadData _saveLoadData;
        
        private IRecorder _recorder;
        private int _levelIndex;
        private Coroutine _checkButtonClickCoroutine;
        private PlayerGameScreen _screen;

        public GameLoopState(ICoroutineRunner coroutineRunner, IPlayerGameScreenProvider playerGameScreenProvider,
            IGameStateMachine stateMachine, ISceneLoader sceneLoader, IPlayerInput playerInput,
            ILevelProgress levelProgress, ICurtain curtain, ILevelContext levelContext,IGameDataContainer gameDataContainer, ISaveLoadData saveLoadData)
        {
            _coroutineRunner = coroutineRunner;
            _playerGameScreenProvider = playerGameScreenProvider;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _playerInput = playerInput;
            _levelProgress = levelProgress;
            _curtain = curtain;
            _levelContext = levelContext;
            _gameDataContainer = gameDataContainer;
            _saveLoadData = saveLoadData;
        }

        public async void Enter()
        {
            _screen = await _playerGameScreenProvider.Load();
            SwitchCursor(false);

            _levelIndex = _levelContext.LevelIndex;
            _checkButtonClickCoroutine = _coroutineRunner.StartCoroutine(CheckButtonClickCoroutine());
            _playerInput.Switch(true);
            _levelProgress.SetCompleteAction(CompleteLevel);
            _levelProgress.SetChangeHiddenObjectViewAction(ChangeHiddenObjectViewAction);
            _recorder ??= GetRecorder();
            _recorder.Play();
        }

        private void CompleteLevel()
        {
            AddCompletedLevel();
            _recorder.Complete();
            OpenMenu();
        }


        public void Exit()
        {
            _coroutineRunner.StopCoroutine(_checkButtonClickCoroutine);
            _playerInput.Switch(false);
            _levelProgress.SetCompleteAction(null);
            _levelProgress.SetChangeHiddenObjectViewAction(null);

            SwitchCursor(true);
            _playerGameScreenProvider.Unload();
        }

        private void ChangeHiddenObjectViewAction(bool canSee)
        {
            _screen.ActivateImage.gameObject.SetActive(canSee);
        }

        private IRecorder GetRecorder()
        {
            var eventName =
                $"playerPosition";
            
            var path = $"{SceneManager.GetActiveScene().name}/{_gameDataContainer.GameData.PlayerData.NickName}_{_gameDataContainer.GameData.GetLevelSessionNumber(_levelContext.LevelIndex)}.json";
            var localPath = $"Assets/TestHeatmap/{path}";
            const float recordInterval = 0.02f;
            var offset = Vector3.down * 0.4f;
            return RecorderFactory.Instance.GetFirebaseRecorder(new RecordeSettingContainer(
                eventName, recordInterval,
                () => _levelContext.PlayerComponent.transform.position + offset), localPath, path);
        }

        private void AddCompletedLevel()
        {
            _gameDataContainer.GameData.AddLevelSessionNumber(_levelContext.LevelIndex);
            _saveLoadData.Save();
        }


        private void SwitchCursor(bool mode)
        {
            Cursor.visible = mode;
            Cursor.lockState = mode ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private IEnumerator CheckButtonClickCoroutine()
        {
            var openMenuOperation = new KeyDownOperation(KeyCode.L, OpenMenu);
            var openTaskOperation = new KeyDownOperation(KeyCode.T, OpenTask);
            var openHelpOperation = new KeyDownOperation(KeyCode.H, OpenHelp);
            do
            {
                yield return null;
            } while (!openTaskOperation.CanActivate && !openMenuOperation.CanActivate && !openHelpOperation.CanActivate);

            if (openMenuOperation.TryActivate())
                yield break;

            if (openTaskOperation.TryActivate())
                yield break;
            
            if (openHelpOperation.TryActivate())
                yield break;
        }

        private void OpenHelp()
        {
            _recorder.Pause();
            _stateMachine.Enter<HelpGameState>();
        }

        private void OpenTask()
        {
            _recorder.Pause();
            _stateMachine.Enter<ShowTaskState, int>(_levelIndex);
        }

        private async void OpenMenu()
        {
            _curtain.Show();
            _recorder.Break();
            _recorder = null;
            await _sceneLoader.LoadScene(StartSceneKey);
            _curtain.Hide();
            _stateMachine.Enter<SelectLevelsState>();
        }


        private class KeyDownOperation
        {
            private readonly KeyCode _keyActivator;
            private readonly Action _operation;
            private bool _isActivated;

            public KeyDownOperation(KeyCode _keyActivator, Action operation)
            {
                this._keyActivator = _keyActivator;
                _operation = operation;
            }

            public bool CanActivate => Input.GetKeyDown(_keyActivator);

            public bool TryActivate()
            {
                if (Input.GetKeyDown(_keyActivator))
                {
                    _isActivated = true;
                    _operation?.Invoke();
                    return true;
                }

                return false;
            }
        }
    }
}