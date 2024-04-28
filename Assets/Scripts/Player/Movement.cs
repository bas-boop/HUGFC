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
        [SerializeField] private Camera playerCamera;
        
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
            Vector3 newVelocity = CalculateDirection(moveInput) * (isRunning ? runSpeed : moveSpeed);

            _rigidbody.velocity = new Vector3(newVelocity.x, _rigidbody.velocity.y, newVelocity.z);
            _currentState = runInput > 0 ? PlayerMoveState.RUNNING : PlayerMoveState.WALKING;
        }

        public void SetIdle()
        {
            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.Divide(deceleration, 1, deceleration);
            _rigidbody.velocity = newVelocity;
            _currentState = PlayerMoveState.IDLE;
        }

        private Vector3 CalculateDirection(Vector2 moveInput)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            Vector3 direction = cameraForward * moveInput.y + cameraRight * moveInput.x;
            
            direction.y = 0;
            direction.Normalize();
            
            return direction;
        }
    }
}