using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class BaseCombatAbility : MonoBehaviour
    {
        [SerializeField] BaseCharacterAnimation characterAnimation;

        protected CombatMoveInfo _currentMoveInfo = null;

        public bool IsAttacking
        {
            get
            {
                return _isAttacking;
            }
        }
        protected bool _isAttacking;

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

        public void ResetAttackData()
        {
            _isAttacking = false;
            _currentMoveInfo = null;
        }

        protected void EndCurrentAttackSuccessfully()
        {
            ResetAttackData();
        }
    }
}
