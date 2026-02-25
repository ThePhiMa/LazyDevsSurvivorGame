using UnityEngine;
using BIMM.Data;

namespace BIMM.Gameplay.Player
{
    public class PlayerCollector : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        private void Update()
        {
            XPGem[] gems = FindObjectsOfType<XPGem>();

            foreach (XPGem gem in gems)
            {
                float distance = Vector2.Distance(transform.position, gem.transform.position);

                if (distance <= _data.CollectionRadius)
                {
                    GetComponent<PlayerXP>().AddXP(gem.XPValue);
                    Destroy(gem.gameObject);
                }
            }
        }
    }
}
