using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class LocationExit : MonoBehaviour
    {
        [Header("Loading settings")]
        [SerializeField] private GameSceneSO[] _locationsToLoad = default;
        [SerializeField] private PathTakenManagerAnchor _pathTakenManagerAnchor = default;
        [SerializeField] private PathSO _exitPath = default;
        [SerializeField] private SceneLoaderAnchor _sceneLoaderAnchor = default;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerCharacter>(out PlayerCharacter pc))
            {
                UpdatePathTaken();
                _sceneLoaderAnchor.GetReference().LoadLocation(_locationsToLoad, true);
            }
        }

        private void UpdatePathTaken()
        {
            _pathTakenManagerAnchor?.GetReference()?.SetPathTaken(_exitPath);
        }
    }
}
