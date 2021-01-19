using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] Movement _movement;
        [SerializeField] Collision _collision;

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
            if (_collision.OnGround)
            {
                if (_movement.IsMovingHorizontally)
                {
                    // play run animation
                    PlayAnimIfNotCurrentlyPlaying(runName);
                }
                else
                {
                    // play idle animation
                    PlayAnimIfNotCurrentlyPlaying(idleName);
                }
            }
            else
            {
                PlayAnimIfNotCurrentlyPlaying(airName);
            }
        }

        bool IsCurrentlyOnState(string animationName)
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        }

        void PlayAnimation(string animationName)
        {
            _animator.Play(animationName, 0, 0);
        }

        void PlayAnimIfNotCurrentlyPlaying(string animationName)
        {
            if (!IsCurrentlyOnState(animationName))
            {
                PlayAnimation(animationName);
            }
        }
    }
}
