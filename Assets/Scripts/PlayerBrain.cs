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
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            _movement.horizontalIntent = _playerControls.Player.Move.ReadValue<float>();
        }
    }
}
