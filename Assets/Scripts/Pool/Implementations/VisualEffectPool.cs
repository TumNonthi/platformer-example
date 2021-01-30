using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class VisualEffectPool : ComponentPool<PoolableVisualEffect>
    {
        [SerializeField] private VisualEffectFactorySO _factory;

        public override IFactory<PoolableVisualEffect> Factory { get => _factory; set => _factory = value as VisualEffectFactorySO; }
    }
}
