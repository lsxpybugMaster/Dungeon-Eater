using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// 与卡牌选中事件的绑定及效果展示有关
/// 该类用于展示卡牌删除/升级时的事件绑定及效果
/// </summary>
public class ShowCardUIBase : MonoBehaviour
{
    //复用事件注册
    public event Action OnCardUIClicked;
    

    //复用跟动画相关的代码

    //存储最初大小,用于Scale动画
    protected Vector3 originCardScale;

    [SerializeField] protected CardUI cardUIPrefab;


    //限制使用的次数(有些子类不需要),如只允许1次卡牌升级的功能
    protected int AvailableTimes = 1;
    public void Setup()
    {
        AvailableTimes = 1;
    }


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
         .SetEase(Ease.OutCubic)
         .SetLink(t.gameObject, LinkBehaviour.KillOnDestroy); //保证不会出现原对象删除导致进入SafeMode
    }


    protected void CardSelectedEffect(RectTransform rect)
    {

        // 防止重复点击叠加 Tween
        rect.DOKill();

        Sequence seq = DOTween.Sequence()
                              .SetLink(rect.gameObject, LinkBehaviour.KillOnDestroy);

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

    //保证杀掉所有 Target / Link 在这个 GameObject 上的 Tween
    protected virtual void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
