using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.HelpGameScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.RecorderScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.Curtain;
using StoryInPuzzle.Infrastructure.Services.Data;
using StoryInPuzzle.Infrastructure.Services.HeatmapRecorder;
using StoryInPuzzle.Infrastructure.Services.LevelContext;
using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;
using StoryInPuzzle.Infrastructure.States;

namespace StoryInPuzzle.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;

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
            serviceContainer.RegisterServiceInterfaces<HeatmapRecorder>();

            serviceContainer.RegisterServiceInterfaces<SceneLoader>();
            serviceContainer.RegisterServiceInterfaces<GameDataContainer>();
            serviceContainer.RegisterServiceInterfaces<SaveLoadData>();
            serviceContainer.RegisterServiceInterfacesFromInstance(new GameConfigProvider(gameConfig));
            serviceContainer.RegisterServiceInterfacesFromInstance(coroutineRunner);
            serviceContainer.RegisterServiceInterfacesFromInstance(playerInput);
            serviceContainer.RegisterServiceInterfacesFromInstance(levelProgress);
            serviceContainer.RegisterServiceInterfacesFromInstance(curtain);

            GameStateMachine = new GameStateMachine(serviceContainer);
            serviceContainer.RegisterServiceInterfacesFromInstance(GameStateMachine);

            GameStateMachine.RegisterState<BootstrapState>();
            GameStateMachine.RegisterState<CheckLoginState>();
            GameStateMachine.RegisterState<LoginState>();
            GameStateMachine.RegisterState<SelectLevelsState>();
            GameStateMachine.RegisterState<LoadLevelState>();
            GameStateMachine.RegisterState<GameLoopState>();
            GameStateMachine.RegisterState<ShowTaskState>();
            GameStateMachine.RegisterState<HelpGameState>();
        }
    }
}