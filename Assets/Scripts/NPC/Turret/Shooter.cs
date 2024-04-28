using UnityEngine;

using Framework;

namespace NPC.Turret
{
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        
        private Hitable _player;

        public void SetTarget(Hitable target) => _player = target;

        public void Shoot()
        {
            if (!_player)
                return;
            
            Debug.DrawLine(firePoint.position, _player.transform.position, Color.red, 10);
        }
    }
}