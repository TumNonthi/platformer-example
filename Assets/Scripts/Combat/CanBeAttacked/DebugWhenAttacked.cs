using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class DebugWhenAttacked : MonoBehaviour, ICanBeAttacked
    {
        public void OnAttacked(CombatActor attacker, Attack attack)
        {
            Debug.Log($"{gameObject.name} was attacked for {attack.Damage} damage.");
        }
    }
}
