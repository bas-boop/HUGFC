using UnityEngine;

namespace NPC.Turret
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private float shootPower;

        private Rigidbody _rigidbody;
        
        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public void GiveTarget(Transform target)
        {
            Vector3 bulletPos = transform.position;
            Vector3 targetPos = target.position;
            Vector3 direction = targetPos - bulletPos;
            
            direction.Normalize();
            direction *= shootPower;

            _rigidbody.velocity = direction;
        }
    }
}