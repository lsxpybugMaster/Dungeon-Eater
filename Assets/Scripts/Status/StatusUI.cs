using TMPro;
using UnityEngine;

/// <summary>
/// 展示状态信息的UI, 继承自ItemUI
/// </summary>
public class StatusUI : ItemUI<StatusData>
{
    [SerializeField] private TMP_Text stackCountText;

    //需要存储动态数据, 可以后续改成 Model
    private int stack;

    /// <summary>
    /// 由于不支持动态数据配置,需要单独提供一个方法来更新状态层数
    /// </summary>
    /// <param name="stackCount"></param>
    public void SetStackCount(int stackCount)
    {
        stack = stackCount;
        stackCountText.text = stackCount.ToString();
    }

    protected override void Refresh(StatusData data)
    {
        image.sprite = data.sprite;
    }

    //TODO: 提取这些与Tooltip相关的逻辑到一个单独的组件中, 以支持其他UI元素显示Tooltip

    public override void OnClickSuccess()
    {
        TooltipManager.Instance?.Hide();
    }

    protected override void MouseEnterEffect()
    {
        Debug.Log("Status UI :: Mouse Enter");

        base.MouseEnterEffect();

        TooltipManager.Instance?.Show(Data.GetTooltipData(stack));
    }

    protected override void MouseExitEffect()
    {
        Debug.Log("Status UI :: Mouse Leave");

        base.MouseExitEffect();

        TooltipManager.Instance?.Hide();
    }
}
