using UnityEngine;

namespace BIMM.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "BIMM/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public float MaxHealth = 30f;
        public float MoveSpeed = 2f;
        public float ContactDamage = 10f;
    }
}
