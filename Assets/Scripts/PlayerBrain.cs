using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyPlatformer
{
    public class PlayerBrain : MonoBehaviour, IMovementInputSource
    {
        [SerializeField] private PlayerControls _playerControls;
        [SerializeField] private Movement _movement;
        [SerializeField] private PlayerCombatAbility _combatAbility;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            if (_movement != null)
            {
                _movement.movementInputSource = this;
            }

            _playerControls.Player.Jump.performed += HandleJump;
            _playerControls.Player.Jump.canceled += HandleCancelJump;
            _playerControls.Player.Attack.performed += HandleAttack;
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            if (_movement != null)
            {
                if (_movement.movementInputSource.Equals(this))
                {
                    _movement.movementInputSource = null;
                }
            }

            _playerControls.Player.Jump.performed -= HandleJump;
            _playerControls.Player.Jump.canceled -= HandleCancelJump;
            _playerControls.Player.Attack.performed -= HandleAttack;
            _playerControls.Disable();
        }

        void HandleJump(InputAction.CallbackContext context)
        {
            if (_playerControls.Player.Move.ReadValue<Vector2>().y < 0f)
            {
                _movement.DropThrough();
            }
            else
            {
                _movement.QueueJump();
            }
        }

        void HandleCancelJump(InputAction.CallbackContext context)
        {
            _movement.CancelJump();
        }

        void HandleAttack(InputAction.CallbackContext context)
        {
            _combatAbility.QueueAttack();
        }

        public Vector2 GetMovementInput()
        {
            return _playerControls.Player.Move.ReadValue<Vector2>();
        }
    }
}
