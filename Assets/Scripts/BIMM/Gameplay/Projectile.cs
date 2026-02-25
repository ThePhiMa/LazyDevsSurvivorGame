using UnityEngine;
using BIMM.Data;
using BIMM.Gameplay.Player;

namespace BIMM.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private WeaponData _data;

        private Vector2 _direction;
        private float _lifetimeTimer;
        private float _damage;

        public void Launch(Vector2 direction, float damage)
        {
            _direction = direction;
            _damage = damage;
            _lifetimeTimer = _data.ProjectileLifetime;
        }

        private void Update()
        {
            GetComponent<Rigidbody2D>().linearVelocity = _direction * _data.ProjectileSpeed;

            _lifetimeTimer -= Time.deltaTime;

            if (_lifetimeTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (damageable != null && playerHealth == null)
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
