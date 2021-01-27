using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public class ManagerLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        public GameSceneSO ManagerSceneSO;
        public bool IsEditorInitializationMode
        {
            get;
            private set;
        }

        private void Awake()
        {
            LoadManagerScene();
        }
        
        void LoadManagerScene()
        {
            Scene managerScene = SceneManager.GetSceneByName(ManagerSceneSO.scenePath);
            if (!managerScene.isLoaded)
            {
                SceneManager.LoadScene(ManagerSceneSO.scenePath, LoadSceneMode.Additive);
                IsEditorInitializationMode = true;
            }
        }
#endif
    }
}
