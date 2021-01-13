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
        [SerializeField] private float jumpBufferTime = 0.2f;
        [SerializeField] private float coyoteTime = 0.1f;
        [SerializeField] private int maxNumberOfJumps = 1;
        [SerializeField] private float dropThroughTime = 0.25f;

        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private CharacterCollision characterCollision;

        [HideInInspector]
        public float horizontalIntent = 0f;

        private Rigidbody2D rb;

        private bool jumpQueued = false;
        private float jumpQueueTime = 0f;
        private bool jumpCancelQueued = false;
        private bool jumpCanceled = false;
        private float jumpTimer = 0f;
        private bool isJumping = false;
        private int numberOfJumps = 1;
        private float notGroundedTimer = 0f;
        private bool wasGrounded = false;
        
        private float dropThroughTimer = 0f;

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
                return characterCollision.OnGround;
            }
        }

        private void Start()
        {
            TryGetComponent(out rb);
        }

        private void Update()
        {
            CheckResetDropThrough(Time.deltaTime);
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
                playerAnimation.Flip(1);
            }
            else if (horizontalIntent < 0f)
            {
                playerAnimation.Flip(-1);
            }
        }

        void CheckGrounded(float dt)
        {
            if (rb.velocity.y <= 0f)
            {
                isJumping = false;
            }

            if (characterCollision.OnGround)
            {
                if (!wasGrounded)
                {
                    OnHitGround();
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

            wasGrounded = characterCollision.OnGround;
        }

        void Walk(float direction)
        {
            direction = Mathf.Clamp(direction, -1f, 1f);

            if (characterCollision.OnGround && !isJumping)
            {
                rb.velocity = new Vector2(direction * speed, Mathf.Min(0f, rb.velocity.y));
            }
            else
            {
                rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            }
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

        void UpdateJumpTimer(float dt)
        {
            if (isJumping)
            {
                jumpTimer += dt;
            }
        }

        void CheckJumpCancel()
        {
            if (jumpCancelQueued && jumpTimer >= jumpMinTime && !jumpCanceled)
            {
                jumpCanceled = true;
                if (rb.velocity.y > 0f && isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cancelJumpMult);
                }
            }
        }

        bool CanJump()
        {
            return numberOfJumps > 0 && dropThroughTimer <= 0f;
        }

        public void QueueJump()
        {
            jumpCancelQueued = false;
            jumpQueued = true;
            jumpQueueTime = Time.time;
        }

        public void CancelJump()
        {
            jumpCancelQueued = true;
        }

        public void DropThrough()
        {
            characterCollision.IgnoreOneWayPlatformAtFeet();
            dropThroughTimer = dropThroughTime;
        }

        void ResetDropThrough()
        {
            characterCollision.ResetIgnoredPlatform();
        }

        void CheckResetDropThrough(float dt)
        {
            if (dropThroughTimer > 0f)
            {
                dropThroughTimer -= dt;
                if (dropThroughTimer <= 0f)
                {
                    ResetDropThrough();
                }
            }
        }
    }
}
