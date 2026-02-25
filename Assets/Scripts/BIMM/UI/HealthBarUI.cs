using UnityEngine;
using UnityEngine.UI;
using BIMM.Gameplay.Player;

namespace BIMM.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        private void Update()
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

            if (playerHealth == null)
            {
                return;
            }

            _fill.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        }
    }
}
