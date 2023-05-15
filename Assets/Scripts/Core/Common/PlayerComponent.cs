using UnityEngine;

namespace Core.Common
{
    public interface IPlayerComponent
    {
        Rigidbody Rigidbody { get; }
        Transform Transform { get; }
        CapsuleCollider CapsuleCollider { get; }
        Camera Camera { get; }
    }

    public class PlayerComponent : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CapsuleCollider capsuleCollider;

        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Rigidbody Rigidbody => rigidbody;
        public Transform Transform => transform;
        public Camera Camera => camera;
    }
}