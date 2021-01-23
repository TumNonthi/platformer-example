using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class ConditionFunction
    {
        public GameObject Target;
        public ConditionFunctionSO OriginSO;

        public abstract float GetValue();

        public abstract void Initialize();
    }
}
