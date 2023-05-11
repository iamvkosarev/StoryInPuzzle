using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen
{
    public class LevelTaskScreen : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _spawnTaskElementParent;
        [SerializeField] private HiddenObjectView _hiddenObjectPrefab;

        public Transform SpawnTaskElementParent => _spawnTaskElementParent;
        public HiddenObjectView HiddenObjectPrefab => _hiddenObjectPrefab;
        public Button CloseButton => _closeButton;
    }
}