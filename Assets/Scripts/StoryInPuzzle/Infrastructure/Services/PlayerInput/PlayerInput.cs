using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.PlayerInput
{
    [CreateAssetMenu(menuName = "StoryInPuzzle/PlayerInput")]
    public class PlayerInput : ScriptableObject, IPlayerInput
    {
        private bool _isWorking;

        public void Switch(bool mode)
        {
            _isWorking = mode;
        }

        public float MouseX => _isWorking ? Input.GetAxis("Mouse X") : 0f;
        public bool GetKeySitDown => _isWorking && Input.GetKeyDown(KeyCode.LeftShift);
        public bool GetKeySitUp => _isWorking && Input.GetKeyUp(KeyCode.LeftShift);
        public float MouseY => _isWorking ? Input.GetAxis("Mouse Y") : 0f;
        public float Horizontal => _isWorking ? Input.GetAxis("Horizontal") : 0f;
        public float Vertical => _isWorking ? Input.GetAxis("Vertical") : 0f;
    }
}