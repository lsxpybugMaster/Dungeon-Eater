using ActionSystemTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于初始化英雄
/// </summary>
public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView {  get; private set; }

    private void OnEnable()
    {
        //注册Reaction,指明在对什么行动做出反应
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreAction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreAction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Setup(HeroState heroState)
    {
        HeroView.Setup(heroState);
    }


    /// <summary>
    /// 对应Setup,将数据返回
    /// </summary>
    public void SaveData()
    {
        HeroView.SaveData();
    }

    //TODO: 移除逻辑

    //Reactions
    private void EnemyTurnPreAction(EnemyTurnGA enemyTurnGA)
    {

        DiscardAllCardsGA discardAllCardsGA = new();

        ActionSystem.Instance.AddReaction(discardAllCardsGA);

        //-----------------------------清空敌人状态------------------------------------
        foreach (var enemy in EnemySystem.Instance.Enemies)
        {
            enemy.UpdateEffectStacks();
        }
    }


    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {

        int burnStacks = HeroView.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }


        //------------------------敌人AI意图计算及获取事件------------------------------
        DecideEnemyIntendGA decideEnemyIntendGA = new DecideEnemyIntendGA();
        ActionSystem.Instance.AddReaction(decideEnemyIntendGA);

        //--------------------------------抽牌事件-------------------------------------

        //注意这里创建GA时初始化了抽牌数量,那么注册的反应也只会抽对应牌的数量
        DrawCardsGA drawCardsGA = new(5);
        /*
          【注意】此时正在Perform EnemyTurnGA, 如果 Perform drawCardsGA 会由于冲突直接不执行!
           所以在 Performer 中只能 通过添加 Reaction 执行动作
           ActionSystem.Instance.Perform(drawCardsGA);
        */
        ActionSystem.Instance.AddReaction(drawCardsGA);


        //------------------------------清空玩家状态------------------------------------
        HeroView.UpdateEffectStacks();

    }
}
