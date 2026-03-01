using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//目前在VictoryUI下
//展示一些 n 选 1 的卡牌选项
public class ShowChoosingCardUI : ShowCardUIBase
{
    [SerializeField] private GameObject uiContent; //父 UI 对象, 用于控制显示/隐藏
    //Layout区域
    [SerializeField] private RectTransform layoutGroup;

    //Optimize: 使用Unity ContentSizeFilter组件来自动调整LayoutGroup的大小, 以适应不同数量的卡牌
    // private float layoutGroupWidth; //如果牌数过多,会增加宽度
    // [SerializeField] private CardUI cardUIPrefab;
    // [SerializeField] private float additionalSpaceSize;
    [SerializeField] private RectTransform targetRectForAni;

    

    //private Vector3 originCardScale;

    private RectTransform uiContentRect;
    private Vector3 originContentScale;

    //存储当前的奖励卡牌信息
    //注意C# 中需要初始化
    private List<CardUI> rewardCardUIList = new();

    private new void Awake()
    {
        base.Awake();
        //layoutGroupWidth = layoutGroup.sizeDelta.x;
        //originCardScale = cardUIPrefab.transform.localScale;
        uiContentRect = uiContent.GetComponent<RectTransform>();
        originContentScale = uiContentRect.localScale;
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

    //在进行新的展示之前:
    //1. 清空之前的卡牌对象
    //2. 激活UI
    private void LogicBeforeShow()
    {
        rewardCardUIList.Clear();
        for (int i = layoutGroup.childCount - 1; i >= 0; i--)
        {
            Destroy(layoutGroup.GetChild(i).gameObject);
        }

        // 恢复UI的大小(之前动画将其大小归零了)
        uiContentRect.localScale = originContentScale;


        uiContent.SetActive(true);
    }

    //在展示结束后:
    //1. 隐藏UI
    //注意是由动画结束的回调调用,而不是直接调用,以保证动画效果完整播放
    private void LogicAfterShowOver()
    {
        Sequence seq = DOTween.Sequence()
                              .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

        // 依序列播放放大、移动和缩小动画
        seq.Append(
            uiContentRect.DOScaleX(0f, 0.35f)
                     .SetEase(Ease.InBack)
        );

        // seq.DestroyOnComplete(rect.gameObject);
        // 提供动画结束后的回调逻辑
        seq.OnComplete(() => {
            uiContent.SetActive(false);
        });

        seq.Play();
    }


    private void ShowReward(RewardCardEvent evt)
    {
        LogicBeforeShow();

        var rewardCardDataList = evt.rewardCards;
        int len = rewardCardDataList.Count;

        // layoutGroup.SetActive(true);
        //如果卡牌比3多,每多1个要增加additionalSpaceSize的宽度

        //layoutGroup.sizeDelta = new Vector2(
        //    layoutGroupWidth + (len - 3) * additionalSpaceSize, 
        //    layoutGroup.sizeDelta.y
        //);

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
            {
                HideCardUIEffect(cardUI.transform);
            }
        }

        //这里清空所有卡牌对象, 以便后续重新应用
        //uiContent.SetActive(false);
        //DisableAllRewardCards();
    }


    protected override void CardSelectedEffect(RectTransform rect)
    {
        // 防止重复点击叠加 Tween
        rect.DOKill();

        Sequence seq = DOTween.Sequence()
                              .SetLink(rect.gameObject, LinkBehaviour.KillOnDestroy);

        // 依序列播放放大、移动和缩小动画
        seq.Append(
            rect.DOScale(originCardScale * 1.2f, 0.5f)
                .SetEase(Ease.OutBack)
        );

        seq.Append(
            rect.DOAnchorPos(Vector2.zero, 0.5f)
                .SetEase(Ease.OutCubic)
        );

        seq.Append(
            rect.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
        );


        // seq.DestroyOnComplete(rect.gameObject);
        // 提供动画结束后的回调逻辑
        seq.OnComplete(() => {
            Destroy(rect.gameObject); //删除该选择的卡牌对象
            LogicAfterShowOver();
        });

        seq.Play();
    }
}
