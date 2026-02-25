using UnityEngine;
using BIMM.Data;

namespace BIMM.Gameplay.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyData _data;
        [SerializeField] private GameObject _xpGemPrefab;

        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _data.MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_xpGemPrefab != null)
            {
                Instantiate(_xpGemPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
