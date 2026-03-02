using UnityEngine;

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
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out pos);

        rect.localPosition = pos + new Vector2(20, -20);
    }
}