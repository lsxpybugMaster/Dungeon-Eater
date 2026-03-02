using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// 鼠标悬浮时的提示信息显示组件
/// </summary>
public class TooltipView : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;

    public void Refresh(TooltipData data)
    {
        titleText.text = data.Title;
        descText.text = data.Description;
    }

    public void Show()
    {
        canvasGroup.DOFade(1, 0.15f);
    }

    public void Hide()
    {
        canvasGroup.DOFade(0, 0.15f);
    }

    public void HideInstant()
    {
        canvasGroup.alpha = 0;
    }
}