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

    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData);
    }

    //Reactions
    private void EnemyTurnPreAction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }


    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        int burnStacks = HeroView.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }

        //注意这里创建GA时初始化了抽牌数量,那么注册的反应也只会抽对应牌的数量
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }
}
