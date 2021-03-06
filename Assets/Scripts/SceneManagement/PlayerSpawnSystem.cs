using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerSpawnSystem : MonoBehaviour
    {
        [Header("Asset References")]
        [SerializeField] private PlayerSpawnSystemAnchor _spawnSystemAnchor = default;
        [SerializeField] private PlayerCharacter _playerPrefab;
        [SerializeField] private PlayerCharacterAnchor _playerCharacterAnchor = default;
        [SerializeField] private PathTakenManagerAnchor _pathTakenManagerAnchor = default;

        [Header("Scene References")]
        [SerializeField] private LocationEntrance _defaultSpawnPoint;

        private LocationEntrance[] _spawnPoints;

        private void OnEnable()
        {
            _spawnSystemAnchor.SetReference(this);
        }

        private void OnDisable()
        {
            if (_spawnSystemAnchor.GetReference() == this)
            {
                _spawnSystemAnchor.SetReference(null);
            }
        }

        [ContextMenu("Spawn Player")]
        public void SpawnPlayer()
        {
            _spawnPoints = FindObjectsOfType<LocationEntrance>();
            LocationEntrance spawnPoint = _defaultSpawnPoint;
            PathSO pathTaken = null;
            pathTaken = _pathTakenManagerAnchor.GetReference()?.GetPathTaken();
            if (pathTaken != null)
            {
                foreach (LocationEntrance aSpawnPoint in _spawnPoints)
                {
                    if (aSpawnPoint.EntrancePath == pathTaken)
                    {
                        spawnPoint = aSpawnPoint;
                    }
                }
            }

            Spawn(spawnPoint);
        }

        void Spawn(LocationEntrance spawnPoint)
        {
            PlayerCharacter pc = InstantiatePlayer(spawnPoint.transform);
            _playerCharacterAnchor.SetReference(pc);

            spawnPoint.RunPlayerSpawnSequence(pc, HandleSpawnSequencesFinished);
        }

        void HandleSpawnSequencesFinished(PlayerCharacter pc)
        {

        }

        PlayerCharacter InstantiatePlayer(Transform spawnTransform)
        {
            if (_playerPrefab == null)
            {
                throw new System.Exception("Player prefab is null.");
            }

            PlayerCharacter playerInstance = Instantiate(_playerPrefab, spawnTransform.position, spawnTransform.rotation);

            return playerInstance;
        }
    }
}
