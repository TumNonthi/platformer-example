using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public class ManagerLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        public GameSceneSO[] ManagerScenes;
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
            List<GameSceneSO> scenesToLoad = new List<GameSceneSO>();

            for (int i = 0; i < ManagerScenes.Length; i++)
            {
                GameSceneSO sceneSO = ManagerScenes[i];

                for (int j = 0; j < SceneManager.sceneCount; j++)
                {
                    Scene scene = SceneManager.GetSceneAt(j);
                    if (scene.path == sceneSO.scenePath)
                    {
                        return;
                    }
                    else if (j == SceneManager.sceneCount - 1)
                    {
                        scenesToLoad.Add(sceneSO);
                        IsEditorInitializationMode = true;
                    }
                }
            }

            foreach (GameSceneSO sceneSO in scenesToLoad)
            {
                SceneManager.LoadScene(sceneSO.scenePath, LoadSceneMode.Additive);
            }
        }
#endif
    }
}
