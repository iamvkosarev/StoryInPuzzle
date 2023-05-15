using Core.Common;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public class SitController : MonoBehaviour, IPlayerMovement
    {
        [SerializeField] private float sitHeight;

        private IPlayerComponent _playerComponent;
        private IPlayerInput _playerInput;
        private float standHeight;

        private bool sit;
        private bool tryStand;

        void IPlayerMovement.Init(IPlayerComponent playerComponent, IPlayerInput playerInput)
        {
            _playerComponent = playerComponent;
            _playerInput = playerInput;
        }

        private void Start()
        {
            standHeight = _playerComponent.Transform.localScale.y;
        }

        private void Update()
        {
            if (_playerInput.GetKeySitDown)
            {
                if (!sit)
                {
                    sit = true;
                    tryStand = false;
                    _playerComponent.Transform.localScale = new Vector3(1f, sitHeight, 1f);
                }
            }

            if (_playerInput.GetKeySitUp)
            {
                if (sit && !tryStand)
                {
                    tryStand = true;
                }
            }

            if (tryStand && sit && CanStand())
            {
                tryStand = false;
                sit = false;
                _playerComponent.Transform.localScale = new Vector3(1f, standHeight, 1f);
            }
        }

        private bool CanStand()
        {
            var startPoint = _playerComponent.Transform.position + _playerComponent.Transform.up *
                (_playerComponent.Transform.localScale.y * _playerComponent.CapsuleCollider.height) / 2f;
            var ratCast = Physics.Raycast(new Ray(startPoint,
                _playerComponent.Transform.up), out var hit);
            if (!ratCast)
                return true;
            return Vector3.Distance(hit.point, startPoint) >= _playerComponent.CapsuleCollider.height / 2f;
        }
    }
}