using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public interface IMovementInputSource
    {
        Vector2 GetMovementInput();
    }
}
