using ActionSystemTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//展示一些 n 选 1 的卡牌选项
public class ShowChoosenCardUI : MonoBehaviour
{
    [SerializeField] private GameObject layoutGroup;
    [SerializeField] private CardUI cardUIPrefab;

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
        layoutGroup.SetActive(true);

        foreach (var cardData in evt.rewardCards)
        {
            CardUI cardUIInst = Instantiate(cardUIPrefab);
            //挂载到指定父节点下管理
            cardUIInst.transform.SetParent(layoutGroup.transform);
            cardUIInst.Setup(new Card(cardData));
        }
    }
}
