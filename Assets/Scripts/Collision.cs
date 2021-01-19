using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Collision : MonoBehaviour
    {
        public LayerMask groundLayer;

        public float groundCheckRadius = 0.25f;
        public Vector2 bottomOffset;

        Collider2D[] _groundColliders;

        public bool OnGround
        {
            get;
            private set;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OnGround = false;
            _groundColliders = Physics2D.OverlapCircleAll((Vector2)transform.position + bottomOffset, groundCheckRadius, groundLayer);
            if (_groundColliders.Length > 0)
            {
                OnGround = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, groundCheckRadius);
        }
    }
}
