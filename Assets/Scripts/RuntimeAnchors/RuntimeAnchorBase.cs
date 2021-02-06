using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class RuntimeAnchorBase<T> : ScriptableObject where T : Object
    {
        [TextArea] public string description;

        private T _ref;
        private bool _isSet = false;
        public bool IsSet
        {
            get
            {
                return _isSet;
            }
        }

        public T GetReference()
        {
            return _ref;
        }

        public void SetReference(T value)
        {
            _ref = value;
            _isSet = value != null;
        }

        protected virtual void OnDisable()
        {
            _ref = null;
            _isSet = false;
        }
    }
}
