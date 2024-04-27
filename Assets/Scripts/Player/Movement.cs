using UnityEngine;

using Framework;
using Framework.Enums;
using Framework.Extensions;

namespace Player
{
    [RequireComponent(typeof(UniversalGroundChecker), 
        typeof(Rigidbody),
        typeof(InputParser))]
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 50)] private float moveSpeed;
        [SerializeField, Range(0.2f, 100)] private float runSpeed;
        [SerializeField] private float deceleration = 2;
        
        private PlayerMoveState _currentState;
        private UniversalGroundChecker _groundChecker;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _groundChecker = GetComponent<UniversalGroundChecker>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Walk(Vector2 moveInput, float runInput)
        {
            if (!_groundChecker.IsGrounded)
                return;

            bool isRunning = runInput > 0;
            Vector2 direction = moveInput * (isRunning ? runSpeed : moveSpeed);
            Vector3 newVelocity = new (direction.x, _rigidbody.velocity.y, direction.y);
            
            _rigidbody.velocity = newVelocity;
            _currentState = runInput > 0 ? PlayerMoveState.RUNNING : PlayerMoveState.WALKING;
        }

        public void SetIdle()
        {
            _currentState = PlayerMoveState.IDLE;
            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.Divide(deceleration, 1, deceleration);
            _rigidbody.velocity = newVelocity;
        }
    }
}