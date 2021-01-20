using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "IsCharacterWalkingCF", menuName = "Conditions/Is Character Walking")]
    public class IsCharacterWalkingSO : ConditionFunctionSO
    {
        protected override ConditionFunction CreateFunction()
        {
            return new IsCharacterWalkingConditionFunction();
        }
    }

    public class IsCharacterWalkingConditionFunction : ConditionFunction
    {
        private Movement _movement;

        public override void Initialize()
        {
            Target.TryGetComponent(out _movement);
        }

        public override float GetValue()
        {
            return _movement.IsMovingHorizontally ? 1f : 0f;
        }
    }
}
