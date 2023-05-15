using UnityEngine;

namespace Core.Common
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CapsuleCollider capsuleCollider;

        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Rigidbody Rigidbody => rigidbody;
        public Camera Camera => camera;
    }
}