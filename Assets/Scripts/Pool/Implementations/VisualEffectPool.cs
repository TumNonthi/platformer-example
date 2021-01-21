using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace MyPlatformer
{
    public class VisualEffectPool : ComponentPool<PoolableVisualEffect>
    {
        [SerializeField] private VisualEffectFactorySO _factory;

        public override IFactory<PoolableVisualEffect> Factory
        {
            get
            {
                return _factory;
            }
            set
            {
                _factory = value as VisualEffectFactorySO;
            }
        }
    }
}
