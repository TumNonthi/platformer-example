using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class GroundImpactEffect : MonoBehaviour
    {
        [SerializeField] ParticleSystem groundImpactParticleSystem;
        [SerializeField] Movement characterMovement;

        private void OnEnable()
        {
            characterMovement.OnHitGround += HandleHitGround;
        }

        private void OnDisable()
        {
            if (characterMovement != null)
            {
                characterMovement.OnHitGround -= HandleHitGround;
            }
        }

        void HandleHitGround()
        {
            groundImpactParticleSystem.Stop();
            groundImpactParticleSystem.Play();
        }
    }
}
