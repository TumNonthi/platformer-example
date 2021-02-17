using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class GroundImpactEffect : MonoBehaviour
    {
        [SerializeField] ParticleSystem groundImpactParticleSystem;
        [SerializeField] Movement characterMovement;

        [SerializeField] float threshold = 1f;

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

        void HandleHitGround(float verticalVelocity)
        {
            if (Mathf.Abs(verticalVelocity) >= threshold)
            {
                groundImpactParticleSystem.Stop();
                groundImpactParticleSystem.Play();
            }
        }
    }
}
