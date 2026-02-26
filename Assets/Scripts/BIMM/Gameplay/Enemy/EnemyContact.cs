using UnityEngine;
using BIMM.Data;

namespace BIMM.Gameplay.Enemy {
    public class EnemyContact : MonoBehaviour {
        [SerializeField] private EnemyData _data;

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Player")) {
                return;
            }
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null) {
                damageable.TakeDamage(_data.ContactDamage);
            }
        }
    }
}
