using System.Collections.Generic;
using StoryInPuzzle.Infrastructure.Services;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen;
using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.Data;
using StoryInPuzzle.Infrastructure.Services.LoadingScreen;
using StoryInPuzzle.Infrastructure.Services.SceneLoader;
using StoryInPuzzle.Infrastructure.States;

namespace StoryInPuzzle.Infrastructure
{
    public class Game
    {
        private readonly ServicesContainer _serviceContainer;
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen, GameConfig gameConfig)
        {
            _serviceContainer = new ServicesContainer();
            _serviceContainer.RegisterServiceInterfaces<LoginScreenProvider>();
            _serviceContainer.RegisterServiceInterfaces<SelectLevelScreenProvider>();
            
            _serviceContainer.RegisterServiceInterfaces<SceneLoader>();
            _serviceContainer.RegisterServiceInterfaces<GameDataContainer>();
            _serviceContainer.RegisterServiceInterfaces<SaveLoadData>();
            _serviceContainer.RegisterServiceInterfacesFromInstance(new GameConfigProvider(gameConfig));
            _serviceContainer.RegisterServiceInterfacesFromInstance(coroutineRunner);
            _serviceContainer.RegisterServiceInterfacesFromInstance(loadingScreen);

            GameStateMachine = new GameStateMachine(_serviceContainer);
            _serviceContainer.RegisterServiceInterfacesFromInstance(GameStateMachine);
            
            GameStateMachine.RegisterState<BootstrapState>();
            GameStateMachine.RegisterState<CheckLoginState>();
            GameStateMachine.RegisterState<LoginState>();
            GameStateMachine.RegisterState<SelectLevelsState>();
        }
    }
}