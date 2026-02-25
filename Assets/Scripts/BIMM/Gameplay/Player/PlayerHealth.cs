using UnityEngine;
using BIMM.Data;

namespace BIMM.Gameplay.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerData _data;

        private float _currentHealth;
        private float _maxHealthBonus;
        private bool _isInvincible;
        private float _invincibilityTimer;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _data.MaxHealth + _maxHealthBonus;

        private void Start()
        {
            _currentHealth = _data.MaxHealth;
        }

        private void Update()
        {
            if (_isInvincible)
            {
                _invincibilityTimer -= Time.deltaTime;

                if (_invincibilityTimer <= 0f)
                {
                    _isInvincible = false;
                }
            }
        }

        public void TakeDamage(float amount)
        {
            if (_isInvincible)
            {
                return;
            }

            _currentHealth -= amount;
            _isInvincible = true;
            _invincibilityTimer = _data.InvincibilityDuration;

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        public void AddMaxHealth(float bonus)
        {
            _maxHealthBonus += bonus;
            _currentHealth += bonus;
        }

        private void Die()
        {
            Debug.Log("Player died!");
            gameObject.SetActive(false);
        }
    }
}
