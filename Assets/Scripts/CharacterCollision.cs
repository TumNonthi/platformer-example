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

        [SerializeField] private Collider2D bodyCollider;

        Collider2D _oneWayPlatform = null;
        public Collider2D OneWayPlatformAtFeet
        {
            get
            {
                return _oneWayPlatform;
            }
        }

        private Collider2D ignoredCollider = null;

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

        public void IgnoreOneWayPlatformAtFeet()
        {
            if (_oneWayPlatform != null)
            {
                ResetIgnoredPlatform();
                Physics2D.IgnoreCollision(bodyCollider, _oneWayPlatform);
                ignoredCollider = _oneWayPlatform;
            }
        }

        public void ResetIgnoredPlatform()
        {
            if (ignoredCollider != null)
            {
                Physics2D.IgnoreCollision(bodyCollider, ignoredCollider, false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, groundCheckRadius);
        }
    }
}
