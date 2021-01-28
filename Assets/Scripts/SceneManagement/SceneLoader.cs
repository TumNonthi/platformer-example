using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatformer
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Asset References")]
        [SerializeField] private SceneLoaderAnchor _sceneLoaderAnchor;

        [Header("Scene References")]
        [SerializeField] private PlayerSpawnSystemAnchor _playerSpawnSystemAnchor;

        [Header("Persistent Manager Scene")]
        [SerializeField] private GameSceneSO _persistentManagersScene = default;

        private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();
        private List<Scene> _scenesToUnload = new List<Scene>();
        private GameSceneSO _activeScene;
        private List<GameSceneSO> _persistentScenes = new List<GameSceneSO>();

        private void OnEnable()
        {
            _sceneLoaderAnchor.SetReference(this);
        }

        private void OnDisable()
        {
            if (_sceneLoaderAnchor.GetReference() == this)
            {
                _sceneLoaderAnchor.SetReference(null);
            }
        }

        public void LoadLocation(GameSceneSO[] locationsToLoad)
        {
            _persistentScenes.Add(_persistentManagersScene);
            AddScenesToUnload();
            LoadScenes(locationsToLoad);
        }

        private void LoadScenes(GameSceneSO[] locationsToLoad)
        {
            _activeScene = locationsToLoad[0];
            UnloadScenes();

            if (_scenesToLoadAsyncOperations.Count == 0)
            {
                for (int i = 0; i < locationsToLoad.Length; i++)
                {
                    string currentScenePath = locationsToLoad[i].scenePath;
                    _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentScenePath, LoadSceneMode.Additive));
                }
            }

            // Load persistent scenes in case some of them were unloaded.
            for (int i = 0; i < _persistentScenes.Count; i++)
            {
                if (!IsSceneLoaded(_persistentScenes[i].scenePath))
                {
                    _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(_persistentScenes[i].scenePath, LoadSceneMode.Additive));
                }
            }
            StartCoroutine(WaitForLoading());
        }

        private IEnumerator WaitForLoading()
        {
            bool _loadingDone = false;

            while (!_loadingDone)
            {
                for (int i = 0; i < _scenesToLoadAsyncOperations.Count; i++)
                {
                    if (!_scenesToLoadAsyncOperations[i].isDone)
                    {
                        break;
                    }
                    else if (i == _scenesToLoadAsyncOperations.Count - 1)
                    {
                        _loadingDone = true;
                        _scenesToLoadAsyncOperations.Clear();
                        _persistentScenes.Clear();
                        break;
                    }
                }
                yield return null;
            }

            SetActiveScene();
        }

        private void SetActiveScene()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(_activeScene.scenePath));
            _playerSpawnSystemAnchor.GetReference().SpawnPlayer();
        }

        private void AddScenesToUnload()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                string scenePath = scene.path;
                for (int j = 0; j < _persistentScenes.Count; j++)
                {
                    if (scenePath != _persistentScenes[j].scenePath)
                    {
                        if (j == _persistentScenes.Count - 1)
                        {
                            _scenesToUnload.Add(scene);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void UnloadScenes()
        {
            if (_scenesToUnload != null)
            {
                for (int i = 0; i < _scenesToUnload.Count; i++)
                {
                    SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
                }
                _scenesToUnload.Clear();
            }
        }

        private bool IsSceneLoaded(string scenePath)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.path == scenePath)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
