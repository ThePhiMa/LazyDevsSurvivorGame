using UnityEngine;
using UnityEngine.InputSystem;
using BIMM.Data;

namespace BIMM.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        private InputSystem_Actions _inputActions;
        private float _speedBonus;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void AddSpeed(float bonus)
        {
            _speedBonus += bonus;
        }

        private void Update()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
            Vector2 velocity = new Vector2(input.x, input.y) * (_data.MoveSpeed + _speedBonus);
            rb.linearVelocity = velocity;

            if (input.x != 0f)
            {
                sr.flipX = input.x < 0f;
            }
        }
    }
}
