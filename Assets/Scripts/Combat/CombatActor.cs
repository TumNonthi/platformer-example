using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CombatActor : MonoBehaviour
    {
        // Create stats for damage calculation here.

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
