using Core.Common;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public class PlayerMover : MonoBehaviour, IPlayerInputUser
    {
        private static PlayerComponent playerComponent => SavingDataProvider.PlayerComponent;

        [SerializeField] private float speed = 0.2f;

        [SerializeField] private float speedUpValue = 2f;

        [SerializeField] private LayerMask playerFilter;

        private IPlayerInput _playerInput;


        private bool hIncreaseMode;
        private bool vIncreaseMode;
        private float hAxisValue;
        private float vAxisValue;
        private bool speedUp;

        public void Init(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Update()
        {
            CheckSpeedUp();
        }

        private void FixedUpdate()
        {
            if (_playerInput != null)
            {
                var vAddingVector = GetAddingVector(_playerInput.Horizontal, playerComponent.transform.right,
                    ref hAxisValue,
                    ref hIncreaseMode);
                var hAddingVector = GetAddingVector(_playerInput.Vertical, playerComponent.transform.forward,
                    ref vAxisValue,
                    ref vIncreaseMode);
                playerComponent.Rigidbody.MovePosition(playerComponent.Rigidbody.position +
                                                       (vAddingVector + hAddingVector).normalized *
                                                       (speed * (!speedUp ? 1f : speedUpValue) * Time.deltaTime));
            }

            playerComponent.Rigidbody.position = new Vector3(playerComponent.Rigidbody.position.x, HeightPos(),
                playerComponent.Rigidbody.position.z);
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
            var halfOfHeight = (playerComponent.transform.localScale.y * playerComponent.CapsuleCollider.height) / 2f;
            var startPoint = playerComponent.transform.position;
            Physics.Raycast(new Ray(startPoint,
                -playerComponent.transform.up), out var hit, 100f, playerFilter);
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