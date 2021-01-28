using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private PlayerBrain _brain;

        public void ToggleInput(bool on)
        {
            _brain.enabled = on;
        }
    }
}
