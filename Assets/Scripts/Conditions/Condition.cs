using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [System.Serializable]
    public class Condition
    {
        [SerializeField] private ConditionFunctionSO leftFunctionSO;
        [SerializeField] private ComparisonOperator comparison;
        [SerializeField] private ConditionFunctionSO rightFunctionSO;
        [SerializeField] private bool defaultValue;
        [SerializeField] private GameObject defaultTarget;

        private Dictionary<ConditionFunctionSO, ConditionFunction> createdFunctionInstances = new Dictionary<ConditionFunctionSO, ConditionFunction>();

        public bool EvaluateResult(GameObject target)
        {
            if (target == null)
            {
                if (defaultTarget != null)
                {
                    target = defaultTarget;
                }
                else
                {
                    Debug.LogError("No target for condition.");
                    return defaultValue;
                }
            }

            if (leftFunctionSO == null || rightFunctionSO == null)
            {
                Debug.LogError("Null condition function.");
                return defaultValue;
            }

            float leftFunctionValue = leftFunctionSO.GetConditionFunction(target, createdFunctionInstances).GetValue();
            float rightFunctionValue = rightFunctionSO.GetConditionFunction(target, createdFunctionInstances).GetValue();

            return Compare(leftFunctionValue, comparison, rightFunctionValue);
        }

        public static bool Compare(float left, ComparisonOperator comparison, float right)
        {
            switch (comparison)
            {
                case ComparisonOperator.LessThan:
                    return left < right;

                case ComparisonOperator.LessThanOrEqualTo:
                    return left <= right;

                case ComparisonOperator.EqualTo:
                    return Mathf.Abs(left - right) < Mathf.Epsilon;

                case ComparisonOperator.NotEqualTo:
                    return Mathf.Abs(left - right) >= Mathf.Epsilon;

                case ComparisonOperator.GreaterThanOrEqualTo:
                    return left >= right;

                default:
                    return left > right;
            }
        }
    }

    public enum ComparisonOperator
    {
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        NotEqualTo,
        GreaterThanOrEqualTo,
        GreaterThan
    }
}
