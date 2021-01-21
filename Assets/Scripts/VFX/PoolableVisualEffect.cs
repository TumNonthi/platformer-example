using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace MyPlatformer
{
    public class PoolableVisualEffect : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _lifetime = 1f;

        private VisualEffect _particleVfx;
        public VisualEffect ParticleVfx
        {
            get
            {
                return _particleVfx;
            }
        }

        private float _startTime;

        private void Awake()
        {
            TryGetComponent(out _particleVfx);
        }

        private void Update()
        {
            if (Time.time - _startTime > _lifetime)
            {
                _particleVfx.Stop();
                gameObject.SetActive(false);
            }
        }

        public void PlayVfx()
        {
            _startTime = Time.time;
            _particleVfx.Play();
        }

        public bool IsDoneUsing()
        {
            return !gameObject.activeSelf;
        }
    }
}
