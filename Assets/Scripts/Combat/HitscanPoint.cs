using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class HitscanPoint : MonoBehaviour, IPoolable
    {
        [SerializeField] private int bufferSize = 16;
        [SerializeField] private float defaultDistance = 20f;

        [SerializeField] private BulletTracer _bulletTracer;

        private int _hitNum;
        private RaycastHit2D[] _raycastHits;

        private List<RaycastHit2D> _result = new List<RaycastHit2D>();

        private void Awake()
        {
            _raycastHits = new RaycastHit2D[bufferSize];
        }

        public void Fire(HitscanSO originSO, float distance, LayerMask layerMask, bool pierce)
        {
            _result.Clear();
            if (distance <= 0f)
            {
                distance = defaultDistance;
            }
            _hitNum = Physics2D.RaycastNonAlloc(transform.position, transform.right, _raycastHits, distance, layerMask);
            for (int i = 0; i < _hitNum; i++)
            {
                _result.Add(_raycastHits[i]);
            }
            _result.Sort((a, b) => a.distance.CompareTo(b.distance));

            Vector3 endPosition = transform.position + (transform.right * distance);

            if (!pierce && _result.Count > 0)
            {
                endPosition = _result[0].point;
            }

            _bulletTracer?.CreateTracer(transform.position, endPosition);

            originSO?.OnHit(this, _result);

            gameObject.SetActive(false);
        }

        public bool IsDoneUsing()
        {
            return !gameObject.activeSelf;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (transform.right * defaultDistance));
        }
    }
}
