using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class ConditionFunction
    {
        internal GameObject _target;
        protected GameObject Target => _target;
        internal ConditionFunctionSO _originSO;
        protected ConditionFunctionSO OriginSO => _originSO;

        public abstract float GetValue();

        public abstract void Initialize();
    }
}
