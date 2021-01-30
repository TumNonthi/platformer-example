using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class HitscanPointPool : ComponentPool<HitscanPoint>
    {
        [SerializeField] private HitscanPointFactorySO _factory;

        public override IFactory<HitscanPoint> Factory { get => _factory; set => _factory = value as HitscanPointFactorySO; }
    }
}
