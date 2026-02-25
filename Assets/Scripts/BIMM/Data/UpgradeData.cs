using UnityEngine;

namespace BIMM.Data
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "BIMM/Upgrade Data")]
    public class UpgradeData : ScriptableObject
    {
        public string DisplayName;
        public string Description;
        public Sprite Icon;

        public float HealthBonus;
        public float SpeedBonus;
        public float DamageBonus;
        public float FireRateBonus;
        public int ProjectileCountBonus;
    }
}
