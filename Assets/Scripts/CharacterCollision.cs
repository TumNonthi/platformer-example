using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CharacterCollision : MonoBehaviour
    {
        public LayerMask groundLayer;

        public bool OnGround
        {
            get;
            private set;
        }

        public float groundCheckRadius = 0.25f;
        public Vector2 bottomOffset;

        public LayerMask oneWayPlatformLayer;

        Collider2D _oneWayPlatform = null;
        public Collider2D OneWayPlatformAtFeet
        {
            get
            {
                return _oneWayPlatform;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OnGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundCheckRadius, groundLayer);

            _oneWayPlatform = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundCheckRadius, oneWayPlatformLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, groundCheckRadius);
        }
    }
}
