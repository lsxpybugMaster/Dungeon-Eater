using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定 Effect 为 RewardEffect, 可以使用接口函数直接获取GA
//NOTE: SR插件 可以直接通过 IamRewardEffect 接口约束可配置的 Effect 类型
/// </summary>
public interface IamRewardEffect
{
    //如果后期参数不够用, 使用param object[]
    public GameAction GetRewardGameAction(int amount);
}

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

    //获取该奖励对应的GameAction, 以便在选择奖励后执行
    public GameAction GetRewardGameAction()
    {
        return rewardData.RewardEffect.GetRewardGameAction(number);
    }
}


//根据当前关卡等级,在合适的范围内生成战利品
public class RewardSystem : Singleton<RewardSystem>
{
    //MVP阶段: 只要求实现随机生成 n 张战利品

    //直接引用该UI, 因为只会有一个, 不需要担心重复引用的问题, 也不需要担心跨场景的问题
    [SerializeField] private ShowRewardUI showRewardUI;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<GetMoneyGA>(GetMoneyPerformer);
        ActionSystem.AttachPerformer<ChooseRewardCardGA>(GetChoosenCardPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<GetMoneyGA>();
        ActionSystem.DetachPerformer<ChooseRewardCardGA>();
    }


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


        //决定额外卡牌奖励
        int cardRewardNumBouns = Config.Instance.basicReward + Random.Range(0,2); //根据当前关卡等级,在合适的范围内生成战利品,暂时先固定为3张
        rewards.Add(
            new RewardContext(
                RewardDataBase.GetRewardDataByType(RewardType.CARDCHOOSE),
                cardRewardNumBouns
            )
        );

        //IMPORTANT: 注意要先Show再绑定事件, 因为Show会刷新实例, 导致原来绑定的事件清空

        showRewardUI.Show(rewards, isGroup:true);
        // ChooseCardLogic();

        //绑定选中卡牌奖励后执行的函数
        showRewardUI.BindOnItemSelectedInGroup(
            (RewardContext rewardContext, int idx) => DoRewardLogic(rewardContext, idx)
        );
    }


    private void DoRewardLogic(RewardContext rewardContext, int idx)
    {
        //获取事件
        GameAction rewardGA = rewardContext.GetRewardGameAction();
        ActionSystem.Instance.Perform(rewardGA);

        //如果确定玩家决定获取, 获取item对象并播放选中效果
        RewardUI rewardUI = showRewardUI.itemUIs[idx].GetComponent<RewardUI>();
        rewardUI.DisableInteraction();
        AnimStatic.ItemScaleAnim(rewardUI.transform, Vector3.zero);
    }    
    

    private void ChooseCardLogic(int choices)
    {
        List<CardData> rewardDatas = new List<CardData>();
        int n = choices; //根据当前关卡等级,在合适的范围内生成战利品
        for (int i = 0; i < n; i++)
        {
            CardData d = CardDatabase.GetRandomCard();
            rewardDatas.Add(d);
        }

        EventBus.Publish(new RewardCardEvent(rewardDatas));
    }


    #region RewardGA 相关事件
    
    private IEnumerator GetMoneyPerformer(GetMoneyGA getMoneyGA)
    {
        GameManager.Instance.HeroState.GainCoins(getMoneyGA.Amount);
        yield return null;
    }

    private IEnumerator GetChoosenCardPerformer(ChooseRewardCardGA chooseRewardCardGA)
    {
        ChooseCardLogic(chooseRewardCardGA.Choices);
        yield return null;
    }

    #endregion
}
