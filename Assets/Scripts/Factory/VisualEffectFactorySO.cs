using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "VisualEffectFactory", menuName = "Factory/VisualEffect Factory")]
    public class VisualEffectFactorySO : FactorySO<PoolableVisualEffect>
    {
        public PoolableVisualEffect prefab = default;

        public override PoolableVisualEffect Create()
        {
            return Instantiate(prefab);
        }
    }
}
