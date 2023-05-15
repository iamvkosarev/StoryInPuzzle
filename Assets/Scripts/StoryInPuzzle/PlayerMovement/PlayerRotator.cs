using Core.Common;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public class PlayerRotator : MonoBehaviour, IPlayerMovement
    {
        [SerializeField] private float xSensitivity = 3f;
        [SerializeField] private float ySensitivity = 3f;
        private IPlayerComponent _playerComponent;
        private IPlayerInput _playerInput;

        void IPlayerMovement.Init(IPlayerComponent playerComponent, IPlayerInput playerInput)
        {
            _playerComponent = playerComponent;
            _playerInput = playerInput;
        }

        void Update()
        {
            if (_playerInput != null)
            {
                _playerComponent.Rigidbody.MoveRotation(_playerComponent.Rigidbody.rotation *
                                                        Quaternion.Euler(new Vector3(0,
                                                            _playerInput.MouseX * xSensitivity, 0)));
                _playerComponent.Camera.transform.eulerAngles -= new Vector3(
                    _playerInput.MouseY * ySensitivity, 0f, 0f);
            }
        }
    }
}