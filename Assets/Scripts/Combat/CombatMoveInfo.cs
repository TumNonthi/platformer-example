using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    [System.Serializable]
    public class CombatMoveInfo
    {
        [SerializeField] private string _animationStateName;
        public string animationStateName
        {
            get
            {
                return _animationStateName;
            }
        }
    }
}
