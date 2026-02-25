using UnityEngine;
using BIMM.Data;
using BIMM.Gameplay.Player;

namespace BIMM.Gameplay.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyData _data;

        private void Update()
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();

            if (player == null)
            {
                return;
            }

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * _data.MoveSpeed;
        }
    }
}
