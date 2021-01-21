using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public interface IPool<T> where T : IPoolable
    {
        void Prewarm(int num);
        T Request();
        void Return(T member);
    }
}
