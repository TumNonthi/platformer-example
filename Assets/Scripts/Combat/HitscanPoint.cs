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
        [SerializeField] private VisualEffectFactorySO _impactVfxFactorySO;
        [SerializeField] private int impactVfxPoolSize = 1;

        private int _hitNum;
        private RaycastHit2D[] _raycastHits;

        private List<RaycastHit2D> _result = new List<RaycastHit2D>();
        
        private VisualEffectPool _impactVfxPool;

        private void Awake()
        {
            _raycastHits = new RaycastHit2D[bufferSize];
            PrepareImpactVfxPool();
        }

        private void PrepareImpactVfxPool()
        {
            if (_impactVfxFactorySO != null)
            {
                _impactVfxPool = new GameObject("Musket Impact VFX Pool").AddComponent<VisualEffectPool>();
                _impactVfxPool.Factory = _impactVfxFactorySO;
                _impactVfxPool.Prewarm(impactVfxPoolSize);
            }
        }

        public void Fire(CombatActor attacker, HitscanSO originSO, float distance, LayerMask layerMask, bool pierce)
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

            if (!pierce && _result.Count > 1)
            {
                _result.RemoveRange(1, _result.Count - 1);
            }

            List<Vector3> targetPositons = new List<Vector3>();
            Vector3 endPosition = transform.position + (transform.right * distance);

            foreach (var hit in _result)
            {
                targetPositons.Add(hit.point);
            }
            if (_result.Count > 0)
            {
                endPosition = _result[_result.Count - 1].point;
            }

            _bulletTracer?.CreateTracer(transform.position, endPosition);
            PlayImpactVfx(targetPositons);

            originSO?.OnHit(attacker, this, _result, transform.right);

            gameObject.SetActive(false);
        }

        private void PlayImpactVfx(List<Vector3> targetPositions)
        {
            if (_impactVfxPool != null)
            {
                foreach (Vector3 target in targetPositions)
                {
                    PoolableVisualEffect vfx = _impactVfxPool.Request();
                    vfx.transform.parent = null;
                    vfx.transform.position = target;
                    vfx.PlayVfx();
                }
            }
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
