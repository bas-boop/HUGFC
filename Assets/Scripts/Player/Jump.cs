using UnityEngine;

using Framework;

namespace Player
{
    [RequireComponent(typeof(InputParser),
        typeof(Rigidbody),
        typeof(UniversalGroundChecker))]
    public sealed class Jump : MonoBehaviour
    {
        [SerializeField] private float jumpPower;
        
        private UniversalGroundChecker _groundChecker;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _groundChecker = GetComponent<UniversalGroundChecker>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ActivateJump()
        {
            if (!_groundChecker.IsGrounded)
                return;

            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.y = jumpPower;
            _rigidbody.velocity = newVelocity;
        }
    }
}