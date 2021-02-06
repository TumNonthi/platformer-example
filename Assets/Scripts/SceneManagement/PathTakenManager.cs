using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PathTakenManager : MonoBehaviour
    {
        [SerializeField] private PathTakenManagerAnchor _pathTakenManagerAnchor;

        private PathSO _pathTaken = null;

        private void OnEnable()
        {
            _pathTakenManagerAnchor.SetReference(this);
        }

        private void OnDisable()
        {
            if (_pathTakenManagerAnchor.GetReference() == this)
            {
                _pathTakenManagerAnchor.SetReference(null);
            }
        }

        public void SetPathTaken(PathSO pathTaken)
        {
            _pathTaken = pathTaken;
        }

        public PathSO GetPathTaken()
        {
            return _pathTaken;
        }
    }
}
