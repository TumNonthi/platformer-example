using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "StaticValueCF", menuName = "Conditions/Static Value")]
    public class StaticValueConditionFunctionSO : ConditionFunctionSO
    {
        [SerializeField] private float value;

        protected override ConditionFunction CreateFunction()
        {
            return new StaticValueConditionFunction(value);
        }
    }

    public class StaticValueConditionFunction : ConditionFunction
    {
        private float _value;

        public StaticValueConditionFunction(float value)
        {
            _value = value;
        }

        public override float GetValue()
        {
            return _value;
        }
    }
}
