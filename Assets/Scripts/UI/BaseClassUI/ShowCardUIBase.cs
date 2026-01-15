using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// 一些需要展示卡牌(含动画)的基类,用于代码复用
/// </summary>
public class ShowCardUIBase : MonoBehaviour
{
    //复用事件注册
    public event Action OnCardUIClicked;


    //复用跟动画相关的代码

    //存储最初大小,用于Scale动画
    protected Vector3 originCardScale;

    [SerializeField] protected CardUI cardUIPrefab;


    protected void Awake()
    {
        originCardScale = cardUIPrefab.transform.localScale;
    }


    //建立卡牌UI点击与调用的联系
    public void RegistCardUI(CardUI cardUI)
    {
        cardUI.OnCardSelected += ShowChoosenCard;
    }

    protected void InvokeOnCardUIClicked()
    {
        OnCardUIClicked?.Invoke();
    }



    public virtual void ShowChoosenCard(Card card)
    {

    }


    protected void ShowCardUIEffect(Transform t)
    {
        CardScaleEffect(t, Vector3.zero, originCardScale);
    }


    protected void HideCardUIEffect(Transform t)
    {
        CardScaleEffect(t, originCardScale, Vector3.zero);
    }


    protected void CardScaleEffect(Transform t, Vector3 fromScale, Vector3 toScale)
    {
        t.localScale = fromScale;

        //防止多次点击叠加 Tween
        //showTween?.Kill();
        //播放 Scale 动画
        //showTween = t

        t.DOScale(toScale, Config.Instance.showCardTime)
         .SetEase(Ease.OutCubic);
    }


    protected void CardSelectedEffect(RectTransform rect)
    {

        // 防止重复点击叠加 Tween
        rect.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(
            rect.DOScale(originCardScale * 1.2f, 0.5f)
                .SetEase(Ease.OutBack)
        );

        seq.Append(
            rect.DOAnchorPos(Vector2.zero, 0.5f)
                .SetEase(Ease.OutCubic)
        );

        //seq.Append(
        //    rect.DOScale(Vector3.zero, 0.5f)
        //        .SetEase(Ease.InBack)
        //);

        seq.Play();
    }

}
