using System.Collections.Generic;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.LoadingScreen;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public class SelectLevelsState : IState
    {
        private readonly ILoadingScreen _loadingScreen;
        private readonly ISelectLevelScreenProvider _selectLevelScreenProvider;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private SelectLevelScreen _screen;

        public SelectLevelsState(ILoadingScreen loadingScreen, ISelectLevelScreenProvider selectLevelScreenProvider,
            IGameConfigProvider gameConfigProvider, IGameStateMachine gameStateMachine)
        {
            _loadingScreen = loadingScreen;
            _selectLevelScreenProvider = selectLevelScreenProvider;
            _gameConfigProvider = gameConfigProvider;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            _loadingScreen.Show();
            _screen = await _selectLevelScreenProvider.Load();
            _loadingScreen.Hide();
            LoadSelectingLevelsViews();
        }

        private void LoadSelectingLevelsViews()
        {
            for (int i = 0; i < _gameConfigProvider.GameConfig.LevelsConfig.LevelsCount; i++)
            {
                var selectingLevelView =
                    Object.Instantiate(_screen.SelectingLevelViewPrefab, _screen.SpawnSelectingLevelsParent);
                selectingLevelView.SetLevel(i + 1);
                selectingLevelView.SetActivatingAction(SelectLevel);
            }
        }

        private void SelectLevel(SelectingLevelView selectingLevelView)
        {
            Debug.Log($"Load level {selectingLevelView.LevelNumber}");
        }

        public void Exit()
        {
        }
    }
}