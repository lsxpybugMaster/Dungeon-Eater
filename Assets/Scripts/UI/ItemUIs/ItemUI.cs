using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemUI<TData> : MonoBehaviour
{
    [Header("交互区域")]
    [SerializeField] protected UIRayCastArea uiRayCastArea;

    [Header("基本视觉属性")]
    [SerializeField] protected Image image;
    [SerializeField] protected Color hoverColor;

    protected Color originColor;
    protected Vector3 oriScale;

    public TData Data { get; private set; }

    // 单个选择
    public event Action<TData> OnItemSelected;

    // 组选择 (例如商店中的多个商品), 传入组内索引以区分
    private int groupIndex;
    public event Action<TData, int> OnItemSelectedInGroup;

    protected virtual void Awake()
    {
        //视觉属性的初始化
        oriScale = transform.localScale;

        if (image != null)
            originColor = image.color;
        
        //交互区域的初始化
        if (uiRayCastArea != null)
        {
            uiRayCastArea.OnHoverEnter += HandleHoverEnter;
            uiRayCastArea.OnHoverExit += HandleHoverExit;
            uiRayCastArea.OnClick += HandleClick;
        }
    }

    protected virtual void OnDestroy()
    {
        OnItemSelected = null;
        OnItemSelectedInGroup = null;
    }

#region Setup

    public void Setup(TData data)
    {
        Data = data;
        Refresh(data); //由于不同UI对象处理并显示数据方式不同,所以使用该抽象方法由子类实现
    }

    public void SetupForGroup(TData data, int index)
    {
        groupIndex = index;
        Setup(data);
    }

#endregion

#region Interaction

    private void HandleHoverEnter() => MouseEnterEffect();
    private void HandleHoverExit() => MouseExitEffect();

    public virtual void HandleClick()
    {   
        OnItemSelected?.Invoke(Data);
        OnItemSelectedInGroup?.Invoke(Data, groupIndex);
    }

    //当玩家点击事件成功执行时立即触发
    public virtual void OnClickSuccess()
    {

    }

    public virtual void DisableInteraction()
    {
        if (uiRayCastArea == null) return;

        uiRayCastArea.OnHoverEnter -= HandleHoverEnter;
        uiRayCastArea.OnHoverExit -= HandleHoverExit;
        uiRayCastArea.OnClick -= HandleClick;
    }

    protected virtual void MouseEnterEffect()
    {
        if (image != null)
            image.color = hoverColor;

        transform.DOScale(oriScale * 1.05f, 0.15f)
                 .SetEase(Ease.OutBack);
    }

    protected virtual void MouseExitEffect()
    {
        if (image != null)
            image.color = originColor;

        transform.DOScale(oriScale, 0.15f)
                 .SetEase(Ease.OutQuad);
    }
#endregion

    /// <summary>
    /// 子类必须实现：如何根据数据刷新UI
    /// </summary>
    protected abstract void Refresh(TData data);
}