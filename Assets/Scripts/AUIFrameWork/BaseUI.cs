using UnityEngine;
using System;

public enum UILayer
{
    DynamicUI, //最顶层
    RoomUI,
    MapUI, //最底层
}

public abstract class BaseUI : MonoBehaviour
{
    public bool IsVisible { get; private set; }
    public abstract UILayer Layer { get; }

    /// <summary>
    /// 子类实现额外的初始化功能
    /// </summary>
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }


    public void Show(object param = null)
    {
        if (IsVisible) return;

        IsVisible = true;
        gameObject.SetActive(true);

        OnShow();

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


    protected virtual void PlayEnterAnimation() { }
    protected virtual void PlayExitAnimation(Action onComplete)
    {
        onComplete?.Invoke();
    }
}
