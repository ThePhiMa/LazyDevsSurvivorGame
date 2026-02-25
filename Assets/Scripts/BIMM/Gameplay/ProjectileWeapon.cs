using System.Linq;
using UnityEngine;
using BIMM.Data;
using BIMM.Gameplay.Enemy;

namespace BIMM.Gameplay
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _data;

        private float _fireTimer;
        private float _damageBonus;
        private float _fireRateBonus;
        private int _projectileCountBonus;

        public void AddDamage(float bonus)
        {
            _damageBonus += bonus;
        }

        public void AddFireRate(float bonus)
        {
            _fireRateBonus += bonus;
        }

        public void AddProjectileCount(int bonus)
        {
            _projectileCountBonus += bonus;
        }

        private void Update()
        {
            _fireTimer -= Time.deltaTime;

            if (_fireTimer > 0f)
            {
                return;
            }

            EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

            EnemyHealth nearest = enemies
                .OrderBy(e => Vector2.Distance(transform.position, e.transform.position))
                .FirstOrDefault();

            if (nearest != null)
            {
                FireAt(nearest.transform);
            }

            _fireTimer = 1f / (_data.FireRate + _fireRateBonus);
        }

        private void FireAt(Transform target)
        {
            Vector2 baseDirection = ((Vector2)target.position - (Vector2)transform.position).normalized;
            int projectileCount = _data.ProjectileCount + _projectileCountBonus;

            for (int i = 0; i < projectileCount; i++)
            {
                float angleOffset = 0f;

                if (projectileCount > 1)
                {
                    angleOffset = Mathf.Lerp(-30f, 30f, (float)i / (projectileCount - 1));
                }

                float rad = angleOffset * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(
                    baseDirection.x * Mathf.Cos(rad) - baseDirection.y * Mathf.Sin(rad),
                    baseDirection.x * Mathf.Sin(rad) + baseDirection.y * Mathf.Cos(rad)
                );

                GameObject projectileObject = Instantiate(_data.ProjectilePrefab, transform.position, Quaternion.identity);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(direction, _data.Damage + _damageBonus);
            }
        }
    }
}
