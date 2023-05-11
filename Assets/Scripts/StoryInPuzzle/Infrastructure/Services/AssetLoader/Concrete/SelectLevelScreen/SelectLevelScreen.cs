using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen
{
    public class SelectLevelScreen : MonoBehaviour
    {
        [SerializeField] private SelectingLevelView _selectingLevelViewPrefab;
        [SerializeField] private Transform _spawnSelectingLevelsParent;
        [SerializeField] private Button changeNameButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI _nicknameText;

        public TextMeshProUGUI NicknameText => _nicknameText;

        public Button ExitButton => exitButton;

        public Button ChangeNameButton => changeNameButton;
        
        public SelectingLevelView SelectingLevelViewPrefab => _selectingLevelViewPrefab;

        public Transform SpawnSelectingLevelsParent => _spawnSelectingLevelsParent;
    }
}