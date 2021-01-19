using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class BaseCombatAbility : MonoBehaviour
    {
        [SerializeField] BaseCharacterAnimation characterAnimation;

        public bool IsAttacking
        {
            get
            {
                return _isAttacking;
            }
        }
        protected bool _isAttacking;

        protected AttackDefinition _currentAttackDefinition = null;

        protected virtual void Update()
        {
            CheckAttackAnimation();
        }

        protected virtual void CheckAttackAnimation()
        {
            if (_currentAttackDefinition != null)
            {
                bool attackEnded = false;

                if (characterAnimation.IsCurrentlyOnState(_currentAttackDefinition.animationStateName))
                {
                    if (characterAnimation.GetAnimationNormalizedTime() >= 1f)
                    {
                        attackEnded = true;
                    }
                }
                else
                {
                    attackEnded = true;
                }

                if (attackEnded)
                {
                    EndCurrentAttackSuccessfully();
                }
            }
        }

        public void StartAttack(AttackDefinition attackDefinition)
        {
            _isAttacking = true;
            characterAnimation.PlayAnimation(attackDefinition.animationStateName);
            _currentAttackDefinition = attackDefinition;
        }

        protected virtual void EndCurrentAttackSuccessfully()
        {
            ResetAttackData();
        }

        public void ResetAttackData()
        {
            _isAttacking = false;
            _currentAttackDefinition = null;
        }
    }
}
