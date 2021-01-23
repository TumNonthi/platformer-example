using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public interface ICanBeAttacked
    {
        void OnAttacked(CombatActor attacker, Attack attack);
    }
}
