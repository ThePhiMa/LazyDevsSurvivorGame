using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BIMM.Data;

namespace BIMM.UI
{
    public class UpgradeOptionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [SerializeField] private TextMeshProUGUI _descriptionLabel;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;

        public void Setup(UpgradeData data, Action<UpgradeData> onSelected)
        {
            _titleLabel.text = data.DisplayName;
            _descriptionLabel.text = data.Description;
            _iconImage.sprite = data.Icon;
            _button.onClick.AddListener(() => onSelected(data));
        }
    }
}
