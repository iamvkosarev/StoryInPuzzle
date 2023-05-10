using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen
{
    public class SelectLevelScreen : MonoBehaviour
    {
        [SerializeField] private SelectingLevelView _selectingLevelViewPrefab;
        [SerializeField] private Transform _spawnSelectingLevelsParent;

        public SelectingLevelView SelectingLevelViewPrefab => _selectingLevelViewPrefab;

        public Transform SpawnSelectingLevelsParent => _spawnSelectingLevelsParent;
    }
}