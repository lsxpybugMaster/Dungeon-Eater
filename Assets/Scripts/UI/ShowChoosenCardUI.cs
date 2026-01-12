using ActionSystemTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

//展示一些 n 选 1 的卡牌选项
public class ShowChoosenCardUI : MonoBehaviour
{
    [SerializeField] private RectTransform layoutGroup;
    private float layoutGroupWidth; //如果牌数过多,会增加宽度
    [SerializeField] private CardUI cardUIPrefab;
    [SerializeField] private float additionalSpaceSize;

    //存储当前的奖励卡牌信息
    //注意C# 中需要初始化
    private List<CardUI> rewardCardUIList = new();

    private void Awake()
    {
        layoutGroupWidth = layoutGroup.sizeDelta.x;
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

    private void DisableAllRewardCards()
    {
        foreach (var cardUI in rewardCardUIList)
        {
           cardUI.DisableCasting();
        }
    }

    //选择一张卡牌,然后隐藏其他卡牌
    private void ChooseCard(Card card, int id)
    {
        GameManager.Instance.PlayerDeckController.AddCardToDeck(card.data);
        Debug.Log($"id号为 [{id}] 的被选中");

        DisableAllRewardCards();
    }
}
