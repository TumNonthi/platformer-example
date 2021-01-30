using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class DestroyWhenAttacked : MonoBehaviour, ICanBeAttacked
    {
        public void OnAttacked(CombatActor attacker, Attack attack)
        {
            Destroy(gameObject);
        }
    }
}
