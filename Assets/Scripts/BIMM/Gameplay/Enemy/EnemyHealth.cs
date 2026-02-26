using UnityEngine;
using BIMM.Data;
using BIMM.Core;

namespace BIMM.Gameplay.Enemy {
    public class EnemyHealth : MonoBehaviour, IDamageable {
        private GameObject _gemWraper;
        [SerializeField] private EnemyData _data;
        [SerializeField] private GameObject _xpGemPrefab;

        private float _currentHealth;

        private void Start() {
            _gemWraper = GameObject.Find("XPGems");
            _currentHealth = _data.MaxHealth;
        }

        public void TakeDamage(float amount) {
            _currentHealth -= amount;

            if (_currentHealth <= 0f) {
                Die();
            }
        }

        private void Die() {
            EnemySpawner.Instance.Pool.Release(gameObject);
            SpawnGem();
        }

        private void SpawnGem() {
            if (_xpGemPrefab != null) {
                Instantiate(_xpGemPrefab, transform.position, Quaternion.identity, _gemWraper.transform);
            }
        }
    }
}
