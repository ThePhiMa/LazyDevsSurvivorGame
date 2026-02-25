using UnityEngine;
using TMPro;

namespace BIMM.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        private void Update()
        {
            float elapsed = Time.timeSinceLevelLoad;
            int minutes = (int)(elapsed / 60f);
            int seconds = (int)(elapsed % 60f);
            _label.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
