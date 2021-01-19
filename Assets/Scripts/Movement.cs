using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Movement : MonoBehaviour
    {
        public delegate void HitGroundDelegate();

        public event HitGroundDelegate OnHitGround;

        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] private float cancelJumpMult = 0f;
        [SerializeField] private float jumpMinTime = 0.1f;
        [SerializeField] private int maxNumberOfJumps = 1;
        [SerializeField] private float jumpBufferTime = 0.2f;
        [SerializeField] private float coyoteTime = 0.1f;

        [SerializeField] private CharacterAnimation _characterAnimation;
        [SerializeField] private Collision _collision;

        public float horizontalIntent;

        private Rigidbody2D rb;

        private bool jumpQueued = false;
        private float jumpQueueTime = 0f;
        private bool jumpCanceledQueue = false;
        private bool jumpCanceled = false;
        private float jumpTimer = 0f;
        private bool isJumping = false;
        private int numberOfJumps = 0;
        private float notGroundedTimer = 0f;
        private bool wasGrounded = false;

        public bool IsMovingHorizontally
        {
            get
            {
                return Mathf.Abs(horizontalIntent) > Mathf.Epsilon;
            }
        }

        public bool IsGrounded
        {
            get
            {
                return _collision.OnGround;
            }
        }

        private void Awake()
        {
            TryGetComponent(out rb);
        }

        private void FixedUpdate()
        {
            CheckGrounded(Time.deltaTime);
            UpdateJumpTimer(Time.deltaTime);
            CheckJumpCancel();

            Walk(horizontalIntent);

            if (jumpQueued)
            {
                if (Time.time - jumpQueueTime <= jumpBufferTime)
                {
                    if (CanJump())
                    {
                        Jump(Vector2.up);
                        jumpQueued = false;
                    }
                }
                else
                {
                    jumpQueued = false;
                }
            }

            if (horizontalIntent > 0f)
            {
                _characterAnimation.Flip(1);
            }
            else if (horizontalIntent < 0f)
            {
                _characterAnimation.Flip(-1);
            }
        }

        void Walk(float direction)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        void CheckGrounded(float dt)
        {
            bool hitGroundThisFrame = false;

            if (rb.velocity.y <= 0f)
            {
                isJumping = false;
            }

            if (_collision.OnGround)
            {
                if (rb.velocity.y <= 0)
                {
                    hitGroundThisFrame = true;
                    if (!wasGrounded)
                    {
                        OnHitGround();
                    }
                }

                if (!isJumping)
                {
                    numberOfJumps = maxNumberOfJumps;
                }

                notGroundedTimer = 0f;
            }
            else
            {
                notGroundedTimer += dt;
                if (notGroundedTimer > coyoteTime)
                {
                    if (numberOfJumps == maxNumberOfJumps)
                    {
                        numberOfJumps--;
                    }
                }
            }

            wasGrounded = hitGroundThisFrame;
        }

        void CheckJumpCancel()
        {
            if (jumpCanceledQueue && jumpTimer >= jumpMinTime && !jumpCanceled)
            {
                jumpCanceled = true;
                if (rb.velocity.y > 0f && isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cancelJumpMult);
                }
            }
        }

        void UpdateJumpTimer(float dt)
        {
            if (isJumping)
            {
                jumpTimer += dt;
            }
        }

        public void QueueJump()
        {
            jumpCanceledQueue = false;
            jumpQueued = true;
            jumpQueueTime = Time.time;
        }

        public void CancelJump()
        {
            jumpCanceledQueue = true;
        }

        // check if we can jump right now
        bool CanJump()
        {
            return numberOfJumps > 0;
        }

        void Jump(Vector2 dir)
        {
            jumpTimer = 0f;
            isJumping = true;
            numberOfJumps--;
            jumpCanceled = false;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.velocity += dir * jumpForce;
        }
    }
}
