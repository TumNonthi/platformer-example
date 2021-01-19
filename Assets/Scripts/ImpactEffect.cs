using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem groundImpactParticleSystem;
        [SerializeField] private Movement _movement;

        private void OnEnable()
        {
            _movement.OnHitGround += HandleHitGround;
        }

        private void OnDisable()
        {
            if (_movement != null)
            {
                _movement.OnHitGround -= HandleHitGround;
            }
        }

        void HandleHitGround()
        {
            groundImpactParticleSystem.Stop();
            groundImpactParticleSystem.Play();
        }
    }
}
