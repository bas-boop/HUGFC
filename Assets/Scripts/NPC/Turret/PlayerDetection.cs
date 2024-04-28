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
        private GameObject _player;

        private void Awake()
        {
            _timer = GetComponent<Timer>();
            _shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
            if (!_player)
                return;
            
            turretHead.LookAt(_player.transform.position);

            if (!Physics.Raycast(transform.position, _player.transform.position - transform.position,
                    out var hit, maxDetectionRange, playerLayer))
                    return;

            Collider hitInfoCollider = hit.collider;

            if (hitInfoCollider == null)
            {
                Debug.Log(":(");
                return;
            }
            
            if (hitInfoCollider.gameObject.name != PLAYER_TAG)
            {
                _timer.ResetTimer();
                Debug.Log(hit.transform.name);
                return;
            }
            
            _timer.SetCanCount(true);
            Debug.Log("yes");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.HasTag(PLAYER_TAG))
                return;

            _player = other.gameObject;
           _shooter.SetTarget(_player.GetComponent<Hitable>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != _player)
                return;
            
            _player = null;
            _shooter.SetTarget(null);
        }
    }
}