using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class FactorySO<T> : ScriptableObject, IFactory<T>
    {
        public abstract T Create();
    }
}
