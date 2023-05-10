using System.Collections.Generic;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.LoadingScreen;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public class SelectLevelsState : IState
    {
        private readonly ICurtain _curtain;
        private readonly ISelectLevelScreenProvider _selectLevelScreenProvider;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private SelectLevelScreen _screen;

        public SelectLevelsState(ICurtain curtain, ISelectLevelScreenProvider selectLevelScreenProvider,
            IGameConfigProvider gameConfigProvider, IGameStateMachine gameStateMachine)
        {
            _curtain = curtain;
            _selectLevelScreenProvider = selectLevelScreenProvider;
            _gameConfigProvider = gameConfigProvider;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            _curtain.Show();
            _screen = await _selectLevelScreenProvider.Load();
            _curtain.Hide();
            LoadSelectingLevelsViews();
            _screen.ChangeNameButton.onClick.AddListener(LoadLoginState);
        }

        private void LoadLoginState()
        {
            _gameStateMachine.Enter<LoginState>();
        }

        private void LoadSelectingLevelsViews()
        {
            for (int i = 0; i < _gameConfigProvider.GameConfig.LevelsConfig.Levels.Count; i++)
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
        }
    }
}