using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CoroutineUtils : MonoBehaviour
    {
        private static CoroutineUtils _instance;
        private static CoroutineUtils Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CoroutineUtils>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("Coroutine Utils");
                        _instance = obj.AddComponent<CoroutineUtils>();
                    }
                }

                return _instance;
            }
        }

        public static bool HasInstance
        {
            get
            {
                return _instance != null;
            }
        }

        public static Coroutine RunCoroutineOnInstance(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }
    }
}
