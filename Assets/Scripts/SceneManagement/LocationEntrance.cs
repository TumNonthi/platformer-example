using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class LocationEntrance : MonoBehaviour
    {
        [Header("Asset References")]
        [SerializeField] private PathSO _entrancePath;

        public PathSO EntrancePath => _entrancePath;

        public void RunPlayerSpawnSequence(PlayerCharacter playerCharacter, System.Action<PlayerCharacter> callback)
        {
            playerCharacter.transform.position = transform.position;
            callback.Invoke(playerCharacter);
        }
    }
}
