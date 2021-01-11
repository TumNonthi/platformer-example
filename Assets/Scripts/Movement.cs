using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 12f;

        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private CharacterCollision characterCollision;

        [HideInInspector]
        public float horizontalIntent = 0f;

        private Rigidbody2D rb;

        private bool jumpQueued = false;
        private bool canJump = false;
        private bool wasGrounded = false;

        private void Start()
        {
            TryGetComponent(out rb);
        }

        private void Update()
        {
            CheckGrounded();

            Walk(horizontalIntent);

            if (jumpQueued)
            {
                jumpQueued = false;
                if (canJump)
                {
                    Jump(Vector2.up);
                }
            }

            if (horizontalIntent > 0f)
            {
                playerAnimation.Flip(1);
            }
            else if (horizontalIntent < 0f)
            {
                playerAnimation.Flip(-1);
            }
        }

        void CheckGrounded()
        {
            if (characterCollision.OnGround)
            {
                if (!wasGrounded)
                {
                    wasGrounded = true;
                    canJump = true;
                }
            }
            else
            {
                wasGrounded = false;
            }
        }

        void Walk(float direction)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        void Jump(Vector2 dir)
        {
            canJump = false;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.velocity += dir * jumpForce;
        }

        public void QueueJump()
        {
            jumpQueued = true;
        }
    }
}
