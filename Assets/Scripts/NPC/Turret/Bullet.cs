using UnityEngine;
using UnityEngine.Events;

namespace NPC.Turret
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Bullet : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        
        [SerializeField] private float shootPower;
        [SerializeField] private float lifeSpan = 10;
        [SerializeField] private int damage = 1;

        [SerializeField] private UnityEvent onPlayerHit = new();
        [SerializeField] private UnityEvent onHit = new();
        
        private Rigidbody _rigidbody;
        private HealthData _targetHealth;
        
        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnTriggerEnter(Collider other)
        {
            Kill();

            if (other.gameObject.HasTag(PLAYER_TAG))
            {
                _targetHealth.TakeDamage(damage);
                onPlayerHit?.Invoke();
            }
            else
                onHit?.Invoke();
        }

        public void GiveTarget(Transform target)
        {
            _targetHealth = target.GetComponent<HealthData>();
            
            ShootSelf(target);
            Invoke(nameof(Kill), lifeSpan);
        }

        private void ShootSelf(Transform target)
        {
            Vector3 bulletPos = transform.position;
            Vector3 targetPos = target.position;
            Vector3 direction = targetPos - bulletPos;
            
            direction.Normalize();
            direction *= shootPower;
            
            _rigidbody.velocity = direction;
        }
        
        private void Kill() => Destroy(gameObject);
    }
}