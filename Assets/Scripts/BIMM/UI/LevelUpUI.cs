using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BIMM.Data;
using BIMM.Gameplay;
using BIMM.Gameplay.Player;

namespace BIMM.UI
{
    public class LevelUpUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private UpgradeOptionUI[] _upgradeOptions;

        private void OnEnable()
        {
            PlayerXP.OnLevelUp += Show;
        }

        private void OnDisable()
        {
            PlayerXP.OnLevelUp -= Show;
        }

        private void Show(int level)
        {
            UpgradeDatabase db = FindObjectOfType<UpgradeDatabase>();

            List<UpgradeData> selected = db.Upgrades
                .OrderBy(_ => Random.value)
                .Take(3)
                .ToList();

            for (int i = 0; i < _upgradeOptions.Length; i++)
            {
                if (i < selected.Count)
                {
                    _upgradeOptions[i].gameObject.SetActive(true);
                    _upgradeOptions[i].Setup(selected[i], SelectUpgrade);
                }
                else
                {
                    _upgradeOptions[i].gameObject.SetActive(false);
                }
            }

            _panel.SetActive(true);
            Time.timeScale = 0f;
        }

        private void SelectUpgrade(UpgradeData data)
        {
            if (data.HealthBonus != 0f)
            {
                FindObjectOfType<PlayerHealth>().AddMaxHealth(data.HealthBonus);
            }

            if (data.SpeedBonus != 0f)
            {
                FindObjectOfType<PlayerMovement>().AddSpeed(data.SpeedBonus);
            }

            if (data.DamageBonus != 0f)
            {
                FindObjectOfType<ProjectileWeapon>().AddDamage(data.DamageBonus);
            }

            if (data.FireRateBonus != 0f)
            {
                FindObjectOfType<ProjectileWeapon>().AddFireRate(data.FireRateBonus);
            }

            if (data.ProjectileCountBonus != 0)
            {
                FindObjectOfType<ProjectileWeapon>().AddProjectileCount(data.ProjectileCountBonus);
            }

            _panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
