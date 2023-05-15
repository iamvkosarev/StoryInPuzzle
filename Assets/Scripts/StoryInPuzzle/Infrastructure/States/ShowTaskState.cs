using System.Threading.Tasks;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen;
using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public sealed class ShowTaskState : IPayloadState<int>
    {
        private readonly ILevelTaskScreenProvider _levelTaskScreenProvider;
        private readonly IGameStateMachine _stateMachine;
        private readonly ILevelProgress _levelProgress;
        private LevelTaskScreen _screen;

        public ShowTaskState(ILevelTaskScreenProvider levelTaskScreenProvider, IGameStateMachine stateMachine, ILevelProgress levelProgress)
        {
            _levelTaskScreenProvider = levelTaskScreenProvider;
            _stateMachine = stateMachine;
            _levelProgress = levelProgress;
        }
        public async void Enter(int data)
        {
            _levelTaskScreenProvider.SetTaskScreenIndex(data);
            _screen = await _levelTaskScreenProvider.Load();
            _screen.CloseButton.onClick.AddListener(CloseTaskScreen);
            LoadHiddenObjectToFind();
        }

        private void LoadHiddenObjectToFind()
        {
            foreach (var hiddenObject in _levelProgress.StillHiddenObjectsList)
            {
                var hiddenObjectView = Object.Instantiate(_screen.HiddenObjectPrefab, _screen.SpawnTaskElementParent);
                hiddenObjectView.Color.color = hiddenObject.Color;
                hiddenObjectView.ObjectName.text = hiddenObject.ObjectName;
            }
        }

        private void CloseTaskScreen()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _screen.CloseButton.onClick.RemoveListener(CloseTaskScreen);
            _levelTaskScreenProvider.Unload();
        }
    }
}