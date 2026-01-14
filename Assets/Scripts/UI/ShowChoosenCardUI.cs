using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


//展示一些 n 选 1 的卡牌选项
public class ShowChoosenCardUI : MonoBehaviour
{
    [SerializeField] private RectTransform layoutGroup;
    private float layoutGroupWidth; //如果牌数过多,会增加宽度
    [SerializeField] private CardUI cardUIPrefab;
    [SerializeField] private float additionalSpaceSize;
    [SerializeField] private RectTransform targetRectForAni;


    private Vector3 originCardScale;

    //存储当前的奖励卡牌信息
    //注意C# 中需要初始化
    private List<CardUI> rewardCardUIList = new();

    private void Awake()
    {
        layoutGroupWidth = layoutGroup.sizeDelta.x;
        originCardScale = cardUIPrefab.transform.localScale;
    }

    private void OnEnable()
    {
        //为了解耦该模块,使用事件总线
        EventBus.Subscribe<RewardCardEvent>(ShowReward);   
    }

    private void OnDisable()
    {
        EventBus.UnSubscribe<RewardCardEvent>(ShowReward);
    }

    private void ShowReward(RewardCardEvent evt)
    {
        var rewardCardDataList = evt.rewardCards;

        // layoutGroup.SetActive(true);
        //如果卡牌比3多,每多1个要增加additionalSpaceSize的宽度
        int len = rewardCardDataList.Count;
        layoutGroup.sizeDelta = new Vector2(
            layoutGroupWidth + (len - 3) * additionalSpaceSize, 
            layoutGroup.sizeDelta.y
        );

        // foreach (var cardData in rewardCardDataList)
        for (int i = 0; i < len; i++)
        {
            CardUI cardUIInst = Instantiate(cardUIPrefab);
            //挂载到指定父节点下管理
            cardUIInst.transform.SetParent(layoutGroup);
            
            cardUIInst.SetupForGroup(new Card(rewardCardDataList[i]), i);

            cardUIInst.OnCardSelectedInGroup += ChooseCard;

            rewardCardUIList.Add(cardUIInst);
        }
    }


    //选择一张卡牌,然后隐藏其他卡牌
    private void ChooseCard(Card card, int id)
    {
        GameManager.Instance.PlayerDeckController.AddCardToDeck(card.data);

        //简单效果
        for (int i = 0; i < rewardCardUIList.Count; i++)
        {
            var cardUI = rewardCardUIList[i];
            cardUI.DisableCasting();
            if (i == id)
            {
                var rect = cardUI.GetComponent<RectTransform>();
                AnimUtil.DetachFromLayoutGroup(rect, GetComponent<RectTransform>());
                CardSelectedEffect(rect);
            }
            else
                CardScaleEffect(cardUI.transform, originCardScale, Vector3.zero);
        }

        //DisableAllRewardCards();
    }

    private void CardScaleEffect(Transform t, Vector3 fromScale, Vector3 toScale)
    {
        t.localScale = fromScale;

        //防止多次点击叠加 Tween
        //showTween?.Kill();

        //播放 Scale 动画
        //showTween = t
        t.DOScale(toScale, Config.Instance.showCardTime)
         .SetEase(Ease.OutCubic);
    }

    private void CardSelectedEffect(RectTransform rect)
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
