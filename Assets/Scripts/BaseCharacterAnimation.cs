using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Flip(int side)
    {
        float angle = side > 0 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public bool IsCurrentlyOnState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public float GetAnimationNormalizedTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName, 0, 0);
    }

    public void PlayAnimIfNotAlreadyPlaying(string animationName)
    {
        if (!IsCurrentlyOnState(animationName))
        {
            PlayAnimation(animationName);
        }
    }
}
