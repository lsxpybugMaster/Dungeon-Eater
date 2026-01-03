using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 专门用来控制鼠标悬浮/点击相关事件,上层结构通过事件调用这些功能
/// 注意同级别中其他RayCast元素要关闭,防止冲突
/// </summary>
public class UIRayCastArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action OnHoverEnter;
    public event Action OnHoverExit;
    public event Action OnClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit?.Invoke();
    }
}
