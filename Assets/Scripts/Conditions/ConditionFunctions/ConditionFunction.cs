using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class ConditionFunction
    {
        internal ConditionFunctionSO _originSO;
        protected ConditionFunctionSO OriginSO => _originSO;

        public abstract float GetValue();

        public virtual void Initialize(GameObject target) { }
    }
}
