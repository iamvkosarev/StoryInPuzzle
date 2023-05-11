using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.HelpGameScreen
{
    public class HelpGameScreen : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        public Button CloseButton => _closeButton;
    }
}