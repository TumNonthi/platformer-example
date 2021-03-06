using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Movement : MonoBehaviour
    {
        public delegate void HitGroundDelegate(float verticalVelocity);

        public event HitGroundDelegate OnHitGround;

        [Header("Horizontal Movement")]
        [SerializeField] private HorizontalMovementProfileSO horizontalMovementProfile;
        [SerializeField] private Condition walkCondition;

        [Space]
        [Header("Jumping")]
        [SerializeField] private JumpProfileSO jumpProfile;
        [SerializeField] private Condition jumpCondition;

        [Space]
        [Header("References")]
        [SerializeField] private BaseCharacterAnimation characterAnimation;
        [SerializeField] private CharacterCollision characterCollision;

        public IMovementInputSource movementInputSource;

        [HideInInspector]
        public float horizontalIntent = 0f;

        private Rigidbody2D rb;

        private bool isWalking = false;
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
                return isWalking;
            }
        }

        public bool IsGrounded
        {
            get
            {
                return characterCollision.OnGround && !isJumping;
            }
        }

        private void Start()
        {
            TryGetComponent(out rb);
        }

        private void Update()
        {
            bool walkConditionResult = walkCondition.EvaluateResult(gameObject);

            RequestMovementInput();
            CheckResetDropThrough(Time.deltaTime);
            CheckGrounded(Time.deltaTime);
            UpdateJumpTimer(Time.deltaTime);
            CheckJumpCancel();

            if (walkConditionResult)
            {
                Walk(horizontalIntent);
                isWalking = Mathf.Abs(horizontalIntent) > Mathf.Epsilon;
            }
            else
            {
                Walk(0f);
                isWalking = false;
            }

            if (jumpQueued)
            {
                if (Time.time - jumpQueueTime <= jumpProfile.JumpBufferTime)
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

            if (walkConditionResult)
            {
                if (horizontalIntent > 0f)
                {
                    characterAnimation.Flip(1);
                }
                else if (horizontalIntent < 0f)
                {
                    characterAnimation.Flip(-1);
                }
            }
        }

        void RequestMovementInput()
        {
            if (movementInputSource != null)
            {
                horizontalIntent = movementInputSource.GetMovementInput().x;
            }
            else
            {
                horizontalIntent = 0f;
            }
        }

        void CheckGrounded(float dt)
        {
            bool hitGroundThisFrame = false;

            if (rb.velocity.y <= 0f)
            {
                isJumping = false;
            }

            if (characterCollision.OnGround)
            {
                if (rb.velocity.y <= 0)
                {
                    hitGroundThisFrame = true;
                    if (!wasGrounded)
                    {
                        OnHitGround?.Invoke(rb.velocity.y);
                    }
                }

                if (!isJumping)
                {
                    numberOfJumps = jumpProfile.MaxNumberOfJumps;
                }

                notGroundedTimer = 0f;
            }
            else
            {
                notGroundedTimer += dt;
                if (notGroundedTimer > jumpProfile.CoyoteTime)
                {
                    if (numberOfJumps == jumpProfile.MaxNumberOfJumps)
                    {
                        numberOfJumps--;
                    }
                }
            }

            wasGrounded = hitGroundThisFrame;
        }

        void Walk(float direction)
        {
            direction = Mathf.Clamp(direction, -1f, 1f);
            if (direction != 0f)
            {
                direction /= Mathf.Abs(direction);
            }

            if (characterCollision.OnGround && !isJumping)
            {
                rb.velocity = new Vector2(direction * horizontalMovementProfile.Speed, Mathf.Min(0f, rb.velocity.y));
            }
            else
            {
                rb.velocity = new Vector2(direction * horizontalMovementProfile.Speed, rb.velocity.y);
            }
        }

        void Jump(Vector2 dir)
        {
            jumpTimer = 0f;
            isJumping = true;
            numberOfJumps--;
            jumpCanceled = false;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.velocity += dir * jumpProfile.JumpForce;
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
            if (jumpCancelQueued && jumpTimer >= jumpProfile.JumpMinTime && !jumpCanceled)
            {
                jumpCanceled = true;
                if (rb.velocity.y > 0f && isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpProfile.CancelJumpMult);
                }
            }
        }

        bool CanJump()
        {
            return numberOfJumps > 0 && jumpCondition.EvaluateResult(gameObject);
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
            dropThroughTimer = jumpProfile.DropThroughTime;
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
