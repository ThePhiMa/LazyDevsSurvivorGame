using UnityEngine;
using TMPro;
using BIMM.Gameplay.Player;

namespace BIMM.UI
{
    public class LevelDisplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        private void Update()
        {
            PlayerXP playerXP = FindObjectOfType<PlayerXP>();

            if (playerXP == null)
            {
                return;
            }

            _label.text = "Level " + playerXP.CurrentLevel;
        }
    }
}
