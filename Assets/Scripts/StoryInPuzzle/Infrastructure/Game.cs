using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.HelpGameScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.RecorderScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.Curtain;
using StoryInPuzzle.Infrastructure.Services.Data;
using StoryInPuzzle.Infrastructure.Services.LevelContext;
using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;
using StoryInPuzzle.Infrastructure.States;

namespace StoryInPuzzle.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, Curtain curtain, GameConfig gameConfig, PlayerInput playerInput,
            LevelProgress levelProgress)
        {
            var serviceContainer = new ServicesContainer();
            serviceContainer.RegisterServiceInterfaces<LoginScreenProvider>();
            serviceContainer.RegisterServiceInterfaces<SelectLevelScreenProvider>();
            serviceContainer.RegisterServiceInterfaces<PlayerGameScreenProvider>();
            serviceContainer.RegisterServiceInterfaces<LevelTaskScreenProvider>();
            serviceContainer.RegisterServiceInterfaces<LevelContext>();
            serviceContainer.RegisterServiceInterfaces<HelpGameScreenProvider>();
            serviceContainer.RegisterServiceInterfaces<RecordeScreenProvider>();

            serviceContainer.RegisterServiceInterfaces<SceneLoader>();
            serviceContainer.RegisterServiceInterfaces<GameDataContainer>();
            serviceContainer.RegisterServiceInterfaces<SaveLoadData>();
            serviceContainer.RegisterServiceInterfacesFromInstance(gameConfig);
            serviceContainer.RegisterServiceInterfacesFromInstance(coroutineRunner);
            serviceContainer.RegisterServiceInterfacesFromInstance(playerInput);
            serviceContainer.RegisterServiceInterfacesFromInstance(levelProgress);
            serviceContainer.RegisterServiceInterfacesFromInstance(curtain);

            StateMachine = new GameStateMachine(serviceContainer);
            serviceContainer.RegisterServiceInterfacesFromInstance(StateMachine);

            StateMachine.RegisterState<BootstrapState>();
            StateMachine.RegisterState<CheckLoginState>();
            StateMachine.RegisterState<LoginState>();
            StateMachine.RegisterState<SelectLevelsState>();
            StateMachine.RegisterState<LoadLevelState>();
            StateMachine.RegisterState<GameLoopState>();
            StateMachine.RegisterState<ShowTaskState>();
            StateMachine.RegisterState<HelpGameState>();
        }
    }
}