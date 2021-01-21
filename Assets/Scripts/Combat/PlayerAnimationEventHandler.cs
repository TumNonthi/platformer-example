using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerAnimationEventHandler : MonoBehaviour
    {
        [SerializeField] private CombatActor _combatActor;
        [SerializeField] private HitscanSO musketAttackSO;
        [SerializeField] private Transform musketFirePoint;
        [SerializeField] private int musketHitscanPoolSize = 1;

        private HitscanPointPool _musketHitscanPointPool;

        private void Awake()
        {
            PrepareMusketHitscanPool();
        }

        public void FireMusket()
        {
            musketAttackSO.Fire(_combatActor, _musketHitscanPointPool, musketFirePoint.position);
        }

        void PrepareMusketHitscanPool()
        {
            _musketHitscanPointPool = new GameObject("Musket Hitscan Point Pool").AddComponent<HitscanPointPool>();
            _musketHitscanPointPool.transform.parent = transform;
            musketAttackSO.PrepareHitscanPointPool(_musketHitscanPointPool, musketHitscanPoolSize);
        }
    }
}
