using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "MultipleCF", menuName = "Conditions/Multiple")]
    public class MultipleConditionFunctionSO : ConditionFunctionSO
    {
        [SerializeField] private ConditionStackEntry[] conditionStackEntries;

        protected override ConditionFunction CreateFunction()
        {
            return new MultipleConditionFunction(conditionStackEntries);
        }
    }

    public class MultipleConditionFunction : ConditionFunction
    {
        private ConditionStackEntry[] _conditionStackEntries;

        public MultipleConditionFunction(ConditionStackEntry[] conditionStackEntries)
        {
            _conditionStackEntries = conditionStackEntries;
        }

        public override float GetValue()
        {
            bool result = true;
            LogicalOperator logicalOperator = LogicalOperator.And;

            foreach (ConditionStackEntry entry in _conditionStackEntries)
            {
                switch (logicalOperator)
                {
                    case LogicalOperator.And:
                        result = result && entry.condition.EvaluateResult(Target);
                        break;
                    default:
                        result = result || entry.condition.EvaluateResult(Target);
                        break;
                }

                logicalOperator = entry.operatorToNext;
            }

            return result ? 1f : 0f;
        }

        public override void Initialize()
        {
        }
    }

    [System.Serializable]
    public struct ConditionStackEntry
    {
        [SerializeField] private Condition _condition;
        public Condition condition
        {
            get
            {
                return _condition;
            }
        }
        [SerializeField] private LogicalOperator _operatorToNext;
        public LogicalOperator operatorToNext
        {
            get
            {
                return _operatorToNext;
            }
        }
    }
}
