using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//卡牌奖励时间
public struct RewardCardEvent
{
    // 激活事件时传入奖励的卡牌
    public List<CardData> rewardCards;

    public RewardCardEvent(List<CardData> rewardCards)
    {
        this.rewardCards = rewardCards;
    }

}
