using UnityEngine;
using UnityEngine.UI;
using BIMM.Gameplay.Player;

namespace BIMM.UI
{
    public class XPBarUI : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        private void Update()
        {
            PlayerXP playerXP = FindObjectOfType<PlayerXP>();

            if (playerXP == null)
            {
                return;
            }

            _fill.fillAmount = playerXP.CurrentXP / playerXP.XPForNextLevel;
        }
    }
}
