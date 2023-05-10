using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.PlayerGameScreen
{
    public class PlayerGameScreen : MonoBehaviour
    {
        [SerializeField] private Button _openMenuButton;
        [SerializeField] private Button _openTaskButton;

        public Button OpenMenuButton => _openMenuButton;
        public Button OpenTaskButton => _openTaskButton;
    }
}