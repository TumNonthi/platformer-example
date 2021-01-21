using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "Hitscan.asset", menuName = "Attack/Hitscan")]
    public class HitscanSO : AttackDefinitionSO
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _pierce;
        [SerializeField] private HitscanPointFactorySO _hitscanPointFactorySO;

        public void Fire(HitscanPointPool poolSO, Vector3 originPosition)
        {
            HitscanPoint hitscanPoint = poolSO.Request();
            hitscanPoint.transform.position = originPosition;
            hitscanPoint.Fire(this, _range, _layer, _pierce);
        }

        public void OnHit(HitscanPoint hitscanPoint, List<RaycastHit2D> hits)
        {
            if (hits.Count > 0)
            {
                Debug.Log($"Hit: {hits[0].collider.gameObject.name}");
            }
            else
            {
                Debug.Log("Hit nothing");
            }
        }

        public void PrepareHitscanPointPool(HitscanPointPool poolSO, int initialSize)
        {
            poolSO.Factory = _hitscanPointFactorySO;
            poolSO.Prewarm(initialSize);
        }
    }
}
