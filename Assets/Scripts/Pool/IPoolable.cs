using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public interface IPoolable
    {
        bool IsDoneUsing();
    }
}
