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

        private void Start()
        {
            TryGetComponent(out rb);
        }

        private void Update()
        {
            Walk(horizontalIntent);
        }

        public void Walk(float direction)
        {
            rb.velocity = new Vector2(direction * speed, 0f);
        }
    }
}
