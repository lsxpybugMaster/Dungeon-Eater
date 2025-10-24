using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//纯Tag
public interface IUIMove
{
    //需要给外部暴露接口
    /// <summary>
    /// 将UIMoveComponent模块中的SwitchModeMoveUI暴露
    /// </summary>
    public void MoveUI();
}

//IDEA: 尝试实现一个组件 "组合优于继承"
public class UIMoveComponent : MonoBehaviour
{
    private Vector2 OriginPosition;
    private Vector2 TargetPosition;
    private RectTransform rectTransform;

    // 用于控制是否是进入(一般进入是要带一些逻辑的) 
    public bool isInOriginPos {get; private set;}
    [SerializeField] private RectTransform TargetDirection; //为了方便使用,费点空间。
    [SerializeField] private float duration;

    private void Awake()
    {
        isInOriginPos = true;
        rectTransform = GetComponent<RectTransform>();
        //NOTE: 记录锚点坐标,移动UI锚点更佳
        OriginPosition = rectTransform.anchoredPosition;
        TargetPosition = TargetDirection.anchoredPosition;
    }

    void Start()
    {
    }

    void Update()
    {

    }

    //由组件的拥有者调用
    /// <summary>
    /// 切换UI的状态(进入/外部等待)
    /// </summary>
    public void SwitchModeMoveUI()
    {
        Vector2 tar = isInOriginPos?  TargetPosition : OriginPosition;
        MoveUITo(tar, duration);
        isInOriginPos = !isInOriginPos;
    }

    private void MoveUITo(Vector2 targetAnchorPos, float duration)
    {
        // 使用 DOAnchorPos 方法
        rectTransform.DOAnchorPos(targetAnchorPos, duration)
            .SetEase(Ease.Linear); // 可选：设置缓动类型，让动画更自然
            //.OnComplete(() => {
            //    Debug.Log("UI 移动完成!");
            //});
    }
}
