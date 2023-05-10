using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.Config.Concrete
{
    [CreateAssetMenu(menuName = "StoryInPuzzle/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private int levelsCount;

        public int LevelsCount => levelsCount;
    }
}