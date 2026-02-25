using UnityEngine;

namespace BIMM.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "BIMM/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public float MaxHealth = 100f;
        public float MoveSpeed = 5f;
        public float CollectionRadius = 2f;
        public float InvincibilityDuration = 0.5f;

        [Header("XP Curve")]
        [Tooltip("XP required to reach each successive level. Index 0 = level 1 -> 2.")]
        public float[] XPThresholds = { 10f, 25f, 50f, 100f, 175f, 275f, 400f, 550f, 725f, 925f };
    }
}
