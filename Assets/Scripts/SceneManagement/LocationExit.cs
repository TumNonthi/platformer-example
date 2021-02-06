using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class LocationExit : MonoBehaviour
    {
        [SerializeField] private GameSceneSO[] _locationsToLoad = default;
        [SerializeField] private PathTakenManagerAnchor _pathTakenManagerAnchor;
        [SerializeField] private PathSO _exitPath;
        [SerializeField] private SceneLoaderAnchor _sceneLoaderAnchor;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            UpdatePathTaken();
            _sceneLoaderAnchor.GetReference().LoadLocation(_locationsToLoad, true);
        }

        private void UpdatePathTaken()
        {
            _pathTakenManagerAnchor?.GetReference()?.SetPathTaken(_exitPath);
        }
    }
}
