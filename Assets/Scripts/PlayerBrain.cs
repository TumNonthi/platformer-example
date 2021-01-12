using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyPlatformer
{
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private PlayerControls _playerControls;
        [SerializeField] private Movement _movement;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Player.Jump.performed += HandleJump;
            _playerControls.Player.Jump.canceled += HandleCancelJump;
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Player.Jump.performed -= HandleJump;
            _playerControls.Player.Jump.canceled -= HandleCancelJump;
            _playerControls.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            _movement.horizontalIntent = _playerControls.Player.Move.ReadValue<Vector2>().x;
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
    }
}
