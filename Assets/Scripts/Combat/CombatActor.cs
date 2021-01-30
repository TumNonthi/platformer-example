using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CombatActor : MonoBehaviour
    {
        // Create stats like HP, Base ATK power, etc.

        public void PerformAttackHit(GameObject target, Attack attack)
        {
            var attackedComponents = target.GetComponentsInChildren(typeof(ICanBeAttacked));
            foreach (ICanBeAttacked a in attackedComponents)
            {
                a.OnAttacked(this, attack);
            }
        }
    }
}
