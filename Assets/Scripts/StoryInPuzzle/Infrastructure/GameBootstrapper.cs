using StoryInPuzzle.Infrastructure.Services.Config;
using StoryInPuzzle.Infrastructure.Services.Curtain;
using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure
{
    using States;

    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain curtain;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private LevelProgress _levelProgress;
        

        private Game _game;
        private static GameBootstrapper Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _game = new Game(this, curtain, _gameConfig, _playerInput, _levelProgress);
            _game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}