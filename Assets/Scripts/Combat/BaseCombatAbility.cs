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

        protected CombatMoveInfo _currentMoveInfo = null;

        protected virtual void Update()
        {
            CheckAttackAnimation();
        }

        protected virtual void CheckAttackAnimation()
        {
            if (_currentMoveInfo != null)
            {
                bool attackEnded = false;

                if (characterAnimation.IsCurrentlyOnState(_currentMoveInfo.animationStateName))
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

        public void StartAttack(CombatMoveInfo moveInfo)
        {
            _isAttacking = true;
            characterAnimation.PlayAnimation(moveInfo.animationStateName);
            _currentMoveInfo = moveInfo;
        }

        protected virtual void EndCurrentAttackSuccessfully()
        {
            ResetAttackData();
        }

        public void ResetAttackData()
        {
            _isAttacking = false;
            _currentMoveInfo = null;
        }
    }
}
