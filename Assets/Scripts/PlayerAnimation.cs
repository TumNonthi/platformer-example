using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Movement _movement;
        [SerializeField] CharacterCollision _characterCollision;
        [SerializeField] string idleName = "Idle";
        [SerializeField] string runName = "Run";
        [SerializeField] string airName = "Air";

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimations(Time.deltaTime);
        }

        public void Flip(int side)
        {
            float angle = side > 0 ? 0f : 180f;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        void UpdateAnimations(float dt)
        {
            if (_characterCollision.OnGround)
            {
                if (Mathf.Abs(_movement.horizontalIntent) > Mathf.Epsilon)
                {
                    PlayAnimIfNotAlreadyPlaying(runName);
                }
                else
                {
                    PlayAnimIfNotAlreadyPlaying(idleName);
                }
            }
            else
            {
                PlayAnimIfNotAlreadyPlaying(airName);
            }
        }

        bool IsCurrentlyOnState(string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }
        
        void PlayAnimation(string animationName)
        {
            animator.Play(animationName, 0, 0);
        }

        void PlayAnimIfNotAlreadyPlaying(string animationName)
        {
            if (!IsCurrentlyOnState(animationName))
            {
                PlayAnimation(animationName);
            }
        }
    }
}
