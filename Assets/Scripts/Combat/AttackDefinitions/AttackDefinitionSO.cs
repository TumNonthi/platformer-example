using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
    public class AttackDefinitionSO : ScriptableObject
    {
        [SerializeField] protected float damage;

        protected virtual Attack CreateAttack(Vector3 hitPosition, Vector3 hitDirection)
        {
            return new Attack(damage, hitPosition, hitDirection);
        }

        public void PerformAttackHit(CombatActor attacker, GameObject target, Attack attack)
        {
            var attackedComponents = target.GetComponentsInChildren(typeof(ICanBeAttacked));
            foreach (ICanBeAttacked a in attackedComponents)
            {
                a.OnAttacked(attacker, attack);
            }
        }
    }
}
