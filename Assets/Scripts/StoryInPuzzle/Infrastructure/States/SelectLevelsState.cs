using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.Curtain;
using StoryInPuzzle.Infrastructure.Services.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StoryInPuzzle.Infrastructure.States
{
    public sealed class SelectLevelsState : IState
    {
        private readonly ICurtain _curtain;
        private readonly ISelectLevelScreenProvider _selectLevelScreenProvider;
        private readonly IGameConfig _gameConfig;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameDataContainer _gameDataContainer;
        private SelectLevelScreen _screen;

        public SelectLevelsState(ICurtain curtain, ISelectLevelScreenProvider selectLevelScreenProvider,
            IGameConfig gameConfig, IGameStateMachine gameStateMachine, IGameDataContainer gameDataContainer)
        {
            _curtain = curtain;
            _selectLevelScreenProvider = selectLevelScreenProvider;
            _gameConfig = gameConfig;
            _gameStateMachine = gameStateMachine;
            _gameDataContainer = gameDataContainer;
        }

        public async void Enter()
        {
            _curtain.Show();
            _screen = await _selectLevelScreenProvider.Load();
            _curtain.Hide();
            LoadSelectingLevelsViews();
            _screen.NicknameText.text = $"Здравствуй, {_gameDataContainer.GameData.PlayerData.NickName}!";
            _screen.ChangeNameButton.onClick.AddListener(LoadLoginState);
            _screen.ExitButton.onClick.AddListener(ExitApp);
        }

        private void LoadLoginState()
        {
            _gameStateMachine.Enter<LoginState>();
        }

        private void LoadSelectingLevelsViews()
        {
            for (int i = 0; i < _gameConfig.LevelsConfig.Levels.Count; i++)
            {
                var selectingLevelView =
                    Object.Instantiate(_screen.SelectingLevelViewPrefab, _screen.SpawnSelectingLevelsParent);
                selectingLevelView.SetLevel(i);
                selectingLevelView.SetActivatingAction(SelectLevel);
            }
        }

        private void SelectLevel(SelectingLevelView selectingLevelView)
        {
            _gameStateMachine.Enter<LoadLevelState, int>(selectingLevelView.LevelIndex);
        }

        public void Exit()
        {
            _selectLevelScreenProvider.Unload();
            _screen.ChangeNameButton.onClick.RemoveListener(LoadLoginState);
            _screen.ExitButton.onClick.RemoveListener(ExitApp);
        }

        private void ExitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}