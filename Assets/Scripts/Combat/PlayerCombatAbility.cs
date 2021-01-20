using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerCombatAbility : BaseCombatAbility
    {
        [SerializeField] private AttackDefinition basicAttackDefinition;
        [SerializeField] private float attackBufferTime = 0.2f;
        [SerializeField] private Condition basicAttackCondition;

        bool attackQueued = false;
        float attackQueueTime = 0f;

        protected override void Update()
        {
            base.Update();

            if (attackQueued)
            {
                if (Time.time - attackQueueTime <= attackBufferTime)
                {
                    if (CanAttack())
                    {
                        StartAttack(basicAttackDefinition);
                        attackQueued = false;
                    }
                }
                else
                {
                    attackQueued = false;
                }
            }
        }

        bool CanAttack()
        {
            return basicAttackCondition.EvaluateResult(gameObject);
        }

        public void QueueAttack()
        {
            attackQueued = true;
            attackQueueTime = Time.time;
        }
    }
}
