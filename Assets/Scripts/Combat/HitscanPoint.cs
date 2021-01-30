using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace MyPlatformer
{
    public class HitscanPoint : MonoBehaviour, IPoolable
    {
        [SerializeField] private AttackDefinitionSO _attackDefinition;
        [SerializeField] private int _bufferSize = 16;
        [SerializeField] private float _range = 20f;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _pierce;
        
        [SerializeField] private VisualEffectFactorySO _impactVfxFactorySO;
        [SerializeField] private int _impactVfxPoolSize = 1;

        private int _hitNum;
        private RaycastHit2D[] _raycastHits;
        private List<RaycastHit2D> _result = new List<RaycastHit2D>();

        private VisualEffectPool _impactVfxPool;

        private void Awake()
        {
            _raycastHits = new RaycastHit2D[_bufferSize];
            PrepareImpactVfxPool();
        }

        private void PrepareImpactVfxPool()
        {
            if (_impactVfxFactorySO != null)
            {
                _impactVfxPool = PoolUtils.CreatePool<VisualEffectPool, PoolableVisualEffect>(
                    "Musket Impact VFX Pool",
                    _impactVfxFactorySO,
                    null,
                    _impactVfxPoolSize);
            }
        }

        public void Fire(CombatActor attacker)
        {
            _result.Clear();

            _hitNum = Physics2D.RaycastNonAlloc(transform.position, transform.right, _raycastHits, _range, _layer);
            for (int i = 0; i < _hitNum; i++)
            {
                _result.Add(_raycastHits[i]);
            }
            _result.Sort((a, b) => a.distance.CompareTo(b.distance));

            if (!_pierce && _result.Count > 1)
            {
                _result.RemoveRange(1, _result.Count - 1);
            }

            List<Vector3> targetPositions = new List<Vector3>();

            foreach (var hit in _result)
            {
                targetPositions.Add(hit.point);
            }

            PlayImpactVfx(targetPositions);

            foreach (RaycastHit2D hit in _result)
            {
                Attack attack = _attackDefinition.CreateAttack(hit.point, transform.right);
                attacker.PerformAttackHit(hit.collider.gameObject, attack);
            }

            gameObject.SetActive(false);
        }

        private void PlayImpactVfx(List<Vector3> targetPositions)
        {
            foreach (Vector3 target in targetPositions)
            {
                PoolableVisualEffect vfx = _impactVfxPool.Request();
                vfx.transform.parent = null;
                vfx.transform.position = target;
                vfx.PlayVfx();
            }
        }

        public bool IsDoneUsing()
        {
            return !gameObject.activeSelf;
        }
    }
}
