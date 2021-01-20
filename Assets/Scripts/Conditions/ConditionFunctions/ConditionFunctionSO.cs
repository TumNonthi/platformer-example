using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class ConditionFunctionSO : ScriptableObject
    {
        public ConditionFunction GetConditionFunction(GameObject target, Dictionary<ConditionFunctionSO, ConditionFunction> createdFunctionInstances)
        {
            ConditionFunction func;
            if (!createdFunctionInstances.TryGetValue(this, out func))
            {
                func = CreateFunction();
                func._target = target;
                func._originSO = this;
                createdFunctionInstances.Add(this, func);
                func.Initialize();
            }

            return func;
        }

        protected abstract ConditionFunction CreateFunction();
    }
}
