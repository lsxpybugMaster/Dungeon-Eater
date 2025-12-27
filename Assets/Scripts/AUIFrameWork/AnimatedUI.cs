using System;
using UnityEngine;


[RequireComponent(typeof(UIMoveAnimator))]
public abstract class AnimatedUI : BaseUI
{
    UIMoveAnimator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<UIMoveAnimator>();
    }

    protected override void PlayEnterAnimation()
    {
        animator?.PlayEnter();
    }

    protected override void PlayExitAnimation(Action onComplete)
    {
        if (animator != null)
            animator.PlayExit(onComplete);
        else
            onComplete?.Invoke();
    }
}