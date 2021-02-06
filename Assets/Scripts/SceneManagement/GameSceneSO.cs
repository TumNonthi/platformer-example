using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract partial class GameSceneSO : ScriptableObject
    {
#if UNITY_EDITOR
        public UnityEditor.SceneAsset sceneAsset;
#endif
        [HideInInspector]
        public string scenePath;

        [TextArea]
        public string shortDescription;
    }
}
