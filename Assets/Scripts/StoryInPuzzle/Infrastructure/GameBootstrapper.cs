using StoryInPuzzle.Infrastructure.Services.Config;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure
{
    using Services.LoadingScreen;
    using States;

    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain curtain;
        [SerializeField] private GameConfig _gameConfig;
        

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, curtain, _gameConfig);
            _game.GameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}