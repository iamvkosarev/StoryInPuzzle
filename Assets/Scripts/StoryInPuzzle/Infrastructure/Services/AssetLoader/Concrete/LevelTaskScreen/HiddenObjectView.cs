using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LevelTaskScreen
{
    public class HiddenObjectView : MonoBehaviour
    {
        [SerializeField] private Image _color;
        [SerializeField] private TextMeshProUGUI _objectName;

        public Image Color => _color;

        public TextMeshProUGUI ObjectName => _objectName;
    }
}