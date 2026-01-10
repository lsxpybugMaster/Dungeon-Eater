using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//根据当前关卡等级,在合适的范围内生成战利品
public class RewardSystem : Singleton<RewardSystem>
{
    //MVP阶段: 只要求实现随机生成n张战利品

    public void GetReward()
    {
        for (int i = 0; i < 3; i++)
        {
            CardData d = CardDatabase.GetRandomCard();
            if (d == null)
            {
                Debug.Log("not FOUND CARD");
            }
            DebugUtil.Cyan($"Reward Card: {d.name}");
        }
    }
}
