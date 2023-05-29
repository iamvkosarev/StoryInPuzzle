using Core.Common;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public class PlayerMover : MonoBehaviour, IPlayerMovement
    {
        [SerializeField] private float speed = 0.2f;

        [SerializeField] private float speedUpValue = 2f;

        [SerializeField] private LayerMask playerFilter;

        private IPlayerInput _playerInput;
        private IPlayerComponent _playerComponent;


        private bool hIncreaseMode;
        private bool vIncreaseMode;
        private float hAxisValue;
        private float vAxisValue;
        private bool speedUp;

        void IPlayerMovement.Init(IPlayerComponent playerComponent, IPlayerInput playerInput)
        {
            _playerComponent = playerComponent;
            _playerInput = playerInput;
        }

        private void Update()
        {
            CheckSpeedUp();
        }

        private void FixedUpdate()
        {
            if(_playerComponent == null) return;
            if (_playerInput != null)
            {
                var vAddingVector = GetAddingVector(_playerInput.Horizontal, _playerComponent.Transform.right,
                    ref hAxisValue,
                    ref hIncreaseMode);
                var hAddingVector = GetAddingVector(_playerInput.Vertical, _playerComponent.Transform.forward,
                    ref vAxisValue,
                    ref vIncreaseMode);
                _playerComponent.Rigidbody.MovePosition(_playerComponent.Rigidbody.position +
                                                       (vAddingVector + hAddingVector).normalized *
                                                       (speed * (!speedUp ? 1f : speedUpValue) * Time.deltaTime));
            }

            _playerComponent.Rigidbody.position = new Vector3(_playerComponent.Rigidbody.position.x, HeightPos(),
                _playerComponent.Rigidbody.position.z);
        }

        private void CheckSpeedUp()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speedUp = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
                speedUp = false;
        }

        private float HeightPos()

        {
            var halfOfHeight = (_playerComponent.Transform.localScale.y * _playerComponent.CapsuleCollider.height) / 2f;
            var startPoint = _playerComponent.Transform.position;
            Physics.Raycast(new Ray(startPoint,
                -_playerComponent.Transform.up), out var hit, 100f, playerFilter);
            return hit.point.y + halfOfHeight;
        }

        private Vector3 GetAddingVector(float axisValue, Vector3 vector, ref float previousValue, ref bool increaseMode)
        {
            increaseMode = Mathf.Abs(axisValue) >= previousValue;

            previousValue = Mathf.Abs(axisValue);
            if (increaseMode)
                return
                    vector * (axisValue * speed);
            return Vector3.zero;
        }

    }
}