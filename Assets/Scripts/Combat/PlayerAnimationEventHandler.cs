using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerAnimationEventHandler : MonoBehaviour
    {
        [SerializeField] private CombatActor _combatActor;
        [SerializeField] private HitscanPointFactorySO _hitscanPointFactorySO;
        [SerializeField] private int _musketHitscanPoolSize = 1;
        [SerializeField] private Transform _musketFirePoint;

        private HitscanPointPool _musketHitscanPointPool;

        private void Awake()
        {
            PrepareMusketHitscanPool();
        }

        public void FireMusket()
        {
            HitscanPoint hitscanPoint = _musketHitscanPointPool.Request();
            hitscanPoint.transform.rotation = _musketFirePoint.rotation;
            hitscanPoint.transform.position = _musketFirePoint.position;
            hitscanPoint.Fire(_combatActor);
        }

        void PrepareMusketHitscanPool()
        {
            _musketHitscanPointPool = PoolUtils.CreatePool<HitscanPointPool, HitscanPoint>(
                "Musket Hitscan Point Pool", 
                _hitscanPointFactorySO, 
                transform, 
                _musketHitscanPoolSize);
        }
    }
}
