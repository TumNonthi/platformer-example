using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "HitscanPointFactory", menuName = "Factory/HitscanPoint Factory")]
    public class HitscanPointFactorySO : FactorySO<HitscanPoint>
    {
        public HitscanPoint prefab = default;

        public override HitscanPoint Create()
        {
            return Instantiate(prefab);
        }
    }
}
