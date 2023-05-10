using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen
{
    public class LoginScreen : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Button sendButton;
        [SerializeField] private TextMeshProUGUI errorMessage;

        public TextMeshProUGUI ErrorMessage => errorMessage;
        public TMP_InputField Input => _input;

        public Button SendButton => sendButton;
    }
}