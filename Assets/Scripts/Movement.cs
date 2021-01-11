using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] private float cancelJumpMult = 0f;
        [SerializeField] private float jumpMinTime = 0.1f;

        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private CharacterCollision characterCollision;

        [HideInInspector]
        public float horizontalIntent = 0f;

        private Rigidbody2D rb;

        private bool jumpQueued = false;
        private bool jumpCanceled = false;
        private float jumpTimer = 0f;
        private bool isJumping = false;
        private bool canJump = false;

        private void Start()
        {
            TryGetComponent(out rb);
        }

        private void Update()
        {
            CheckGrounded();
            UpdateJumpTimer(Time.deltaTime);
            CheckJumpCancel();

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
            if (rb.velocity.y <= 0f)
            {
                isJumping = false;
            }

            if (characterCollision.OnGround)
            {
                if (!isJumping)
                {
                    canJump = true;
                }
            }
            else
            {
                canJump = false;
            }
        }

        void Walk(float direction)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        void Jump(Vector2 dir)
        {
            jumpCanceled = false;
            jumpTimer = 0f;
            isJumping = true;
            canJump = false;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.velocity += dir * jumpForce;
        }

        void UpdateJumpTimer(float dt)
        {
            if (isJumping)
            {
                jumpTimer += dt;
            }
        }

        void CheckJumpCancel()
        {
            if (jumpCanceled && jumpTimer >= jumpMinTime)
            {
                jumpCanceled = false;
                if (rb.velocity.y > 0f && isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cancelJumpMult);
                }
            }
        }

        public void QueueJump()
        {
            jumpQueued = true;
        }

        public void CancelJump()
        {
            jumpCanceled = true;
        }
    }
}
