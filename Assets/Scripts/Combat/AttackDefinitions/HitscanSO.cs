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

        public void Fire(CombatActor attacker, HitscanPointPool poolSO, Vector3 originPosition)
        {
            HitscanPoint hitscanPoint = poolSO.Request();
            hitscanPoint.transform.position = originPosition;
            hitscanPoint.Fire(attacker, this, _range, _layer, _pierce);
        }

        public void OnHit(CombatActor attacker, HitscanPoint hitscanPoint, List<RaycastHit2D> hits, Vector3 hitscanDirection)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Attack attack = CreateAttack(hit.point, hitscanDirection);
                PerformAttackHit(attacker, hit.collider.gameObject, attack);
            }
        }

        public void PrepareHitscanPointPool(HitscanPointPool poolSO, int initialSize)
        {
            poolSO.Factory = _hitscanPointFactorySO;
            poolSO.Prewarm(initialSize);
        }
    }
}
