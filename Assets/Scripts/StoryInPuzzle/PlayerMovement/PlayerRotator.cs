using Core.Common;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public class PlayerRotator : MonoBehaviour, IPlayerInputUser
    {
        private static PlayerComponent playerComponent => SavingDataProvider.PlayerComponent;
        private IPlayerInput _playerInput;
        [SerializeField] private float xSensitivity = 3f;
        [SerializeField] private float ySensitivity = 3f;

        public void Init(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        void Update()
        {
            if (_playerInput != null)
            {
                playerComponent.Rigidbody.MoveRotation(playerComponent.Rigidbody.rotation *
                                                       Quaternion.Euler(new Vector3(0,
                                                           _playerInput.MouseX * xSensitivity, 0)));
                playerComponent.Camera.transform.eulerAngles -= new Vector3(
                    _playerInput.MouseY * ySensitivity, 0f, 0f);
            }
        }
    }
}