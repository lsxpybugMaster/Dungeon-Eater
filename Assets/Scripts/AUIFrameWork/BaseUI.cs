using UnityEngine;
using System;

public enum UILayer
{
    Global,
    Page,
    Popup,
    Overlay
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
