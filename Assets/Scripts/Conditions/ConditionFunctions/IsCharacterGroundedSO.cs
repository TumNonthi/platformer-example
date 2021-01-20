using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "IsCharacterGroundedCF", menuName = "Conditions/Is Character Grounded")]
    public class IsCharacterGroundedSO : ConditionFunctionSO
    {
        protected override ConditionFunction CreateFunction()
        {
            return new IsCharacterGroundedConditionFunction();
        }
    }

    public class IsCharacterGroundedConditionFunction : ConditionFunction
    {
        private Movement _movement;

        public override void Initialize(GameObject target)
        {
            target.TryGetComponent(out _movement);
        }

        public override float GetValue()
        {
            return _movement.IsGrounded ? 1f : 0f;
        }
    }
}
