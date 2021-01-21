using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Attack
    {
        public float Damage { get; private set; }
        public Vector3 HitPosition { get; private set; }
        public Vector3 AttackDirection { get; private set; }

        public Attack(float damage, Vector3 hitPosition, Vector3 attackDirection)
        {
            Damage = damage;
            HitPosition = hitPosition;
            AttackDirection = attackDirection;
        }
    }
}
