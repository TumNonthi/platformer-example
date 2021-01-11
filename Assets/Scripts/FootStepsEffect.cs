using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class FootStepsEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem footStepsParticleSystem;
        private ParticleSystem.EmissionModule footStepsEmissionModule;

        [SerializeField] private float emissionRate = 30f;

        [SerializeField] private Movement characterMovement;

        // Start is called before the first frame update
        void Start()
        {
            footStepsEmissionModule = footStepsParticleSystem.emission;
        }

        // Update is called once per frame
        void Update()
        {
            if (characterMovement.IsMovingHorizontally && characterMovement.IsGrounded)
            {
                footStepsEmissionModule.rateOverTime = emissionRate;
            }
            else
            {
                footStepsEmissionModule.rateOverTime = 0f;
            }
        }
    }
}
