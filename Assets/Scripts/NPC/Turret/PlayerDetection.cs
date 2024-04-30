using Framework;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

using Timer = Framework.Timer;

namespace NPC.Turret
{
    [RequireComponent(typeof(Collider),
        typeof(Timer),
        typeof(Shooter))]
    public sealed class PlayerDetection : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        [SerializeField] private Transform turretHead;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float maxDetectionRange;

        private Timer _timer;
        private Shooter _shooter;
        private GameObject _target;

        private void Awake()
        {
            _timer = GetComponent<Timer>();
            _shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
            if (_target)
                DetectTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.HasTag(PLAYER_TAG))
                return;

            _target = other.gameObject;
           _shooter.SetTarget(_target.GetComponent<Hitable>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != _target)
                return;
            
            _target = null;
            _shooter.SetTarget(null);
        }

        private void DetectTarget()
        {
            turretHead.LookAt(_target.transform.position);

            if (!Physics.Raycast(transform.position, _target.transform.position - transform.position,
                    out var hit, maxDetectionRange, playerLayer))
                return;

            Collider hitInfoCollider = hit.collider;

            if (hitInfoCollider == null)
                return;
            
            if (hitInfoCollider.gameObject.name != PLAYER_TAG)
            {
                _timer.ResetTimer();
                return;
            }

            _timer.SetCanCount(true);
        }
    }
}