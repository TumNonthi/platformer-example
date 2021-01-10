using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        public float horizontalIntent = 0f;

        private Rigidbody2D rb;

        [SerializeField] private PlayerAnimation playerAnimation;

        private void Start()
        {
            TryGetComponent(out rb);
        }

        private void Update()
        {
            Walk(horizontalIntent);

            if (horizontalIntent > 0f)
            {
                playerAnimation.Flip(1);
            }
            else if (horizontalIntent < 0f)
            {
                playerAnimation.Flip(-1);
            }
        }

        public void Walk(float direction)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
    }
}
