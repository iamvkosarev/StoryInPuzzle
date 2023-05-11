using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.HelpGameScreen;

namespace StoryInPuzzle.Infrastructure.States
{
    public class HelpGameState : IState
    {
        private readonly IHelpGameScreenProvider _helpGameScreenProvider;
        private readonly IGameStateMachine _stateMachine;
        private HelpGameScreen _screen;

        public HelpGameState(IHelpGameScreenProvider helpGameScreenProvider, IGameStateMachine stateMachine)
        {
            _helpGameScreenProvider = helpGameScreenProvider;
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            _screen = await _helpGameScreenProvider.Load();
            _screen.CloseButton.onClick.AddListener(CloseScreen);
        }

        private void CloseScreen()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _screen.CloseButton.onClick.RemoveListener(CloseScreen);
            _helpGameScreenProvider.Unload();
        }
    }
}