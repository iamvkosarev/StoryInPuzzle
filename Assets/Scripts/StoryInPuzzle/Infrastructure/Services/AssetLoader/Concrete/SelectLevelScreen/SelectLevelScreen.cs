using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen
{
    public class SelectLevelScreen : MonoBehaviour
    {
        [SerializeField] private SelectingLevelView _selectingLevelViewPrefab;
        [SerializeField] private Transform _spawnSelectingLevelsParent;
        [SerializeField] private Button changeNameButton;

        public Button ChangeNameButton => changeNameButton;
        
        public SelectingLevelView SelectingLevelViewPrefab => _selectingLevelViewPrefab;

        public Transform SpawnSelectingLevelsParent => _spawnSelectingLevelsParent;
    }
}