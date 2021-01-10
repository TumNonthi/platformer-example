using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Movement : MonoBehaviour
    {
        public void Move(Vector2 displacement)
        {
            transform.Translate(displacement);
        }
    }
}
