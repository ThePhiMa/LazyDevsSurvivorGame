using UnityEngine;

namespace BIMM.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "BIMM/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public float Damage = 25f;
        public float FireRate = 1f;
        public float ProjectileSpeed = 8f;
        public float ProjectileLifetime = 3f;
        public int ProjectileCount = 1;
        public GameObject ProjectilePrefab;
    }
}
