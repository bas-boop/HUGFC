using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Framework;

namespace NPC.Turret
{
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Bullet spanwableBullet;
        [SerializeField] private Light shootLight;
        [SerializeField] private float shootDelay = 0.2f;
        [SerializeField, Range(1, 3)] private int amountBulletToShoot;
        
        [SerializeField] private UnityEvent onShoot = new();
        
        private Hitable _player;
        private int _currentShootAmount;

        private void Start() => shootLight.gameObject.SetActive(false);

        public void SetTarget(Hitable target) => _player = target;

        public void Shoot()
        {
            if (!_player)
                return;

            Bullet currentBullet = Instantiate(spanwableBullet, firePoint.position, firePoint.rotation, transform);
            currentBullet.GiveTarget(_player.transform);

            onShoot?.Invoke();
            
            if (_currentShootAmount < amountBulletToShoot - 1)
            {
                _currentShootAmount++;
                Invoke(nameof(Shoot), shootDelay);
            }
            else
                _currentShootAmount = 0;
        }

        public void ShootLight() => StartCoroutine(Switch());

        private IEnumerator Switch()
        {
            shootLight.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            shootLight.gameObject.SetActive(false);
        }
    }
}