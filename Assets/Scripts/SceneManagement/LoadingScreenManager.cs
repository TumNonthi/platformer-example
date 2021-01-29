using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class LoadingScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject loadingInterface;

        public void ToggleLoadingScreen(bool state)
        {
            loadingInterface.SetActive(state);
        }
    }
}
