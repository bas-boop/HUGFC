using UnityEngine;

using Framework;
using Framework.Enums;

namespace Player
{
    [RequireComponent(typeof(UniversalGroundChecker), 
        typeof(Rigidbody),
        typeof(InputParser))]
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 100)] private float moveSpeed;
        
        private PlayerMoveState _currentState;
        private UniversalGroundChecker _groundChecker;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _groundChecker = GetComponent<UniversalGroundChecker>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Walk(Vector2 input)
        {
            if (!_groundChecker.IsGrounded)
                return;
            
            Vector2 direction = input * moveSpeed;
            Vector3 newVelocity = new (direction.x, _rigidbody.velocity.y, direction.y);
            
            _rigidbody.velocity = newVelocity;
        }
    }
}