using System.Collections.Generic;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.Config.Concrete
{
    [CreateAssetMenu(menuName = "StoryInPuzzle/Config/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<string> _levels;

        public List<string> Levels => _levels;
    }
}