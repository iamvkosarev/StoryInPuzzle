using System;
using StoryInPuzzle.Infrastructure.Services.Config.Concrete;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.Config
{
    [Serializable]
    public class GameConfig : IGameConfig
    {
        [SerializeField] private LevelsConfig _levelsConfig;

        public LevelsConfig LevelsConfig => _levelsConfig;
    }
}