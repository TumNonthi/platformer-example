using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class EditorSpawnPlayer : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private ManagerLoader _managerLoader;
        [SerializeField] private PlayerSpawnSystemAnchor _spawnSystemAnchor;

        private void Start()
        {
            if (_managerLoader.IsEditorInitializationMode)
            {
                _spawnSystemAnchor.GetReference().SpawnPlayer();
            }
        }
#endif
    }
}
