using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public class ManagerLoader : MonoBehaviour
    {
        public GameSceneSO ManagerSceneSO;

        private void Awake()
        {
            LoadManagerScene();
            Destroy(gameObject);
        }
        
        void LoadManagerScene()
        {
            Scene managerScene = SceneManager.GetSceneByName(ManagerSceneSO.scenePath);
            if (!managerScene.isLoaded)
            {
                SceneManager.LoadScene(ManagerSceneSO.scenePath, LoadSceneMode.Additive);
            }
        }
    }
}
