using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//根据当前关卡等级,在合适的范围内生成战利品
public class RewardSystem : Singleton<RewardSystem>
{
    //MVP阶段: 只要求实现随机生成 n 张战利品

    public void GetReward()
    {
        List<CardData> rewardDatas = new List<CardData>();
        int n = Random.Range(3, 6);
        for (int i = 0; i < n; i++)
        {
            CardData d = CardDatabase.GetRandomCard();
            rewardDatas.Add(d);
        }

        EventBus.Publish(new RewardCardEvent(rewardDatas));
    }
}
