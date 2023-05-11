using System.Collections.Generic;
using StoryInPuzzle.Infrastructure.Services;
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
        private readonly ServicesContainer _serviceContainer;
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, Curtain curtain, GameConfig gameConfig, PlayerInput playerInput,
            LevelProgress levelProgress)
        {
            _serviceContainer = new ServicesContainer();
            _serviceContainer.RegisterServiceInterfaces<LoginScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<SelectLevelScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<PlayerGameScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<LevelTaskScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<LevelContext>();
            _serviceContainer.RegisterServiceInterfaces<HelpGameScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<RecordeScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<HeatmapRecorder>();

            _serviceContainer.RegisterServiceInterfaces<SceneLoader>();
            _serviceContainer.RegisterServiceInterfaces<GameDataContainer>();
            _serviceContainer.RegisterServiceInterfaces<SaveLoadData>();
            _serviceContainer.RegisterServiceInterfacesFromInstance(new GameConfigProvider(gameConfig));
            _serviceContainer.RegisterServiceInterfacesFromInstance(coroutineRunner);
            _serviceContainer.RegisterServiceInterfacesFromInstance(playerInput);
            _serviceContainer.RegisterServiceInterfacesFromInstance(levelProgress);
            _serviceContainer.RegisterServiceInterfacesFromInstance(curtain);

            GameStateMachine = new GameStateMachine(_serviceContainer);
            _serviceContainer.RegisterServiceInterfacesFromInstance(GameStateMachine);

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