using UnityEngine;

using Framework;

namespace NPC.Turret
{
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Bullet spanwableBullet;
        [SerializeField] private float shootDelay = 0.2f;
        [SerializeField, Range(1, 3)] private int amountBulletToShoot;
        
        private Hitable _player;
        private int _currentShootAmount;

        public void SetTarget(Hitable target) => _player = target;

        public void Shoot()
        {
            if (!_player)
                return;
            
            // Debug.DrawLine(firePoint.position, _player.transform.position, Color.red, 10);

            Bullet currentBullet = Instantiate(spanwableBullet, firePoint.position, firePoint.rotation, transform);
            currentBullet.GiveTarget(_player.transform);

            if (_currentShootAmount < amountBulletToShoot - 1)
            {
                _currentShootAmount++;
                Invoke(nameof(Shoot), shootDelay);
            }
            else
                _currentShootAmount = 0;
        }
    }
}