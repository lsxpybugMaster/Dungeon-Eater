using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 使用class封装提示信息
/// </summary>
public class TooltipData
{
    public string Title;
    public string Description;

    public TooltipData(string title, string desc)
    {
        Title = title;
        Description = desc;
    }
}


/// <summary>
/// 实现该接口的组件可以提供TooltipData数据, 以供TooltipManager显示
/// </summary>
public interface ITooltipProvider
{
    TooltipData GetTooltipData();
}


public class TooltipManager : PersistentSingleton<TooltipManager>
{
    [SerializeField] private TooltipView tooltipView;

    [Header("位置占右侧多大比例时, 将UI朝向左侧")]
    [SerializeField][Range(0,1)] private float rightPivotThreshold = 0.5f;

    [Header("位置从下到上多大比例时, 将UI朝向上面")]
    [SerializeField][Range(0, 1)] private float downPivotThreshold = 0.5f;

    private RectTransform rect;
    private Canvas canvas;

    protected override void Awake()
    {
        base.Awake();
        rect = tooltipView.GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        tooltipView.HideInstant();
    }


    private void Update()
    {
        FollowMouse();
    }


    public void Show(TooltipData data)
    {
        tooltipView.Refresh(data);
        tooltipView.Show();
    }

    public void Hide()
    {
        tooltipView.Hide();
    }

    private void FollowMouse()
    {
        //Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    canvas.transform as RectTransform,
        //    Input.mousePosition,
        //    canvas.worldCamera,
        //    out pos);

        //rect.localPosition = pos + new Vector2(20, -20);

        Vector2 mouse = Input.mousePosition;

        //刷新内容后强制重建布局, 防止size获取错误
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Vector2 size = rect.sizeDelta;

        bool isRight = mouse.x > Screen.width * rightPivotThreshold;
        bool isTop   = mouse.y > Screen.height * downPivotThreshold;

        rect.pivot = new Vector2(
            isRight ? 1 : 0,
            isTop ? 1 : 0
        );

        rect.position = mouse;
    }
}