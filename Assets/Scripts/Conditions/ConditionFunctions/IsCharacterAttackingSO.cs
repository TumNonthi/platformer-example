using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "IsCharacterAttackingCF", menuName = "Conditions/Is Character Attacking")]
    public class IsCharacterAttackingSO : ConditionFunctionSO
    {
        protected override ConditionFunction CreateFunction()
        {
            return new IsCharacterAttackingConditionFunction();
        }
    }

    public class IsCharacterAttackingConditionFunction : ConditionFunction
    {
        private BaseCombatAbility _combatAbility;

        public override void Initialize()
        {
            Target.TryGetComponent(out _combatAbility);
        }

        public override float GetValue()
        {
            return _combatAbility.IsAttacking ? 1f : 0f;
        }
    }
}
