using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public static class PoolUtils
    {
        public static PoolType CreatePool<PoolType, PoolableType>(string poolName, FactorySO<PoolableType> factory, Transform poolParent, int initialSize) where PoolType : ComponentPool<PoolableType> where PoolableType : Component, IPoolable
        {
            PoolType pool = new GameObject(poolName).AddComponent<PoolType>();
            pool.transform.parent = poolParent;
            pool.Factory = factory;
            pool.Prewarm(initialSize);

            return pool;
        }
    }
}
