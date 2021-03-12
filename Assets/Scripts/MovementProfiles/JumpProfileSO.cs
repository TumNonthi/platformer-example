using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(menuName = "Movement/Jump Profile")]
    public class JumpProfileSO : ScriptableObject
    {
        [SerializeField] private float _jumpForce = 12f;
        public float JumpForce { get { return _jumpForce; } }

        [Range(0f, 1f)]
        [SerializeField] private float _cancelJumpMult = 0f;
        public float CancelJumpMult { get { return _cancelJumpMult; } }

        [SerializeField] private float _jumpMinTime = 0.1f;
        public float JumpMinTime { get { return _jumpMinTime; } }

        [SerializeField] private float _jumpBufferTime = 0.2f;
        public float JumpBufferTime { get { return _jumpBufferTime; } }

        [SerializeField] private float _coyoteTime = 0.1f;
        public float CoyoteTime { get { return _coyoteTime; } }

        [SerializeField] private int _maxNumberOfJumps = 1;
        public int MaxNumberOfJumps { get { return _maxNumberOfJumps; } }

        [SerializeField] private float _dropThroughTime = 0.25f;
        public float DropThroughTime { get { return _dropThroughTime; } }
    }
}
