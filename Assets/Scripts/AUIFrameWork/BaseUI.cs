using UnityEngine;
using System;

public enum UILayer
{
    Global,
    Page,
    Popup,
    Overlay
}

public abstract class GlobalUI : BaseUI
{
    public override UILayer Layer => UILayer.Global;
}


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


public abstract class PageUI : AnimatedUI
{
    public override UILayer Layer => UILayer.Page;
}

public abstract class PopupUI : AnimatedUI
{
    public override UILayer Layer => UILayer.Popup;
}


public abstract class BaseUI : MonoBehaviour
{
    public bool IsVisible { get; private set; }
    public abstract UILayer Layer { get; }

    /// <summary>
    /// 注入依赖（子类实现）
    /// </summary>
    protected virtual void Inject() { }

    public void Show(object param = null)
    {
        if (IsVisible) return;

        Inject();

        IsVisible = true;
        gameObject.SetActive(true);

        OnShow(param);
        PlayEnterAnimation();
    }

    public void Hide()
    {
        if (!IsVisible) return;

        IsVisible = false;

        PlayExitAnimation(() =>
        {
            OnHide();
            gameObject.SetActive(false);
        });
    }

    protected virtual void OnShow(object param) { }
    protected virtual void OnHide() { }

    protected virtual void PlayEnterAnimation() { }
    protected virtual void PlayExitAnimation(Action onComplete)
    {
        onComplete?.Invoke();
    }
}
