using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
    public class AttackDefinitionSO : ScriptableObject
    {
        [SerializeField] protected float damage;

        public virtual Attack CreateAttack(Vector3 hitPosition, Vector3 hitDirection)
        {
            return new Attack(damage, hitPosition, hitDirection);
        }
    }
}
