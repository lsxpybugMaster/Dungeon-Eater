using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

//战利品数据上下文: 包含动态数据 + 静态信息
public class RewardContext
{
    //唯一的动态数据: 数值
    //如果直接使用SO, 任何修改都会污染原SO, 需要区分静态数据和动态数据
    public int number; //依据对应的rewardType, number的意义不同, 例如rewardType为GOLD时number表示金币数量, rewardType为CARDCHOOSE时number表示可选择的卡牌数量

    public readonly RewardData rewardData;

    public string Description => rewardData.GetDescription(number);

    public RewardContext(RewardData _rewardData, int num)
    {
        rewardData = _rewardData;
        number = num;
    }
}


//根据当前关卡等级,在合适的范围内生成战利品
public class RewardSystem : Singleton<RewardSystem>
{
    //MVP阶段: 只要求实现随机生成 n 张战利品
    [SerializeField] private ShowRewardUI showRewardUI;

    public void GetReward()
    {
        List<RewardContext> rewards = new();

        //决定金币奖励
        int coinReward = Random.Range(10, 30);
        rewards.Add(
            new RewardContext(
                RewardDataBase.GetRewardDataByType(RewardType.GOLD), 
                coinReward
            )
        );


        //决定额外金币奖励
        int extraCoinReward = Random.Range(5, 30);
        if (extraCoinReward > 15) { }
        else rewards.Add(
            new RewardContext(
                RewardDataBase.GetRewardDataByType(RewardType.GOLD),
                extraCoinReward
            )
        ); 
        

        //决定卡牌奖励
        int cardRewardNum = Config.Instance.basicReward; //根据当前关卡等级,在合适的范围内生成战利品,暂时先固定为3张
        rewards.Add(
            new RewardContext(
                RewardDataBase.GetRewardDataByType(RewardType.CARDCHOOSE),
                cardRewardNum
            )
        );

        showRewardUI.Show(rewards, isGroup:false);

        ChooseCardLogic();
    }

    private void ChooseCardLogic()
    {
        List<CardData> rewardDatas = new List<CardData>();
        int n = Config.Instance.basicReward; //根据当前关卡等级,在合适的范围内生成战利品,暂时先固定为3张
        for (int i = 0; i < n; i++)
        {
            CardData d = CardDatabase.GetRandomCard();
            rewardDatas.Add(d);
        }

        EventBus.Publish(new RewardCardEvent(rewardDatas));
    }       
}
