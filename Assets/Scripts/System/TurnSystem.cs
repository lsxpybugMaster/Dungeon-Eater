using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与回合有关的事件Performer,便于进行管理
/// </summary>
public class TurnSystem : Singleton<TurnSystem>
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<UpdateEffectGA>(UpdateEffectPerformer);

        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreAction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);  
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<UpdateEffectGA>();

        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreAction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }


    //-------------------------------敌人回合前(作为反应)---------------------------------
    private void EnemyTurnPreAction(EnemyTurnGA enemyTurnGA)
    {
        //-----------------------------清空敌人状态------------------------------------
        foreach (var enemy in EnemySystem.Instance.Enemies)
        {
            UpdateEffectGA updateEffectGA = new(enemy);
            ActionSystem.Instance.AddReaction(updateEffectGA);
        }


        DiscardAllCardsGA discardAllCardsGA = new();

        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    //--------------------------------敌人回合----------------------------------
    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        //到了敌人的回合,遍历每个敌人并执行逻辑
        foreach (var enemy in EnemySystem.Instance.Enemies)
        {
            //-------------------------结算当前状态-----------------------------
            int burnStacks = enemy.GetStatusEffectStacks(StatusEffectType.BURN);
            if (burnStacks > 0)
            {
                ApplyBurnGA applyBurnGA = new(burnStacks, enemy);
                ActionSystem.Instance.AddReaction(applyBurnGA);
            }

            //AttackHeroGA attackHeroGA = new(enemy);
            //ActionSystem.Instance.AddReaction(attackHeroGA);
            //-------------------------执行AI系统-----------------------------
            EnemySystem.Instance.DoEnemyIntend(enemy); //底层是AddReaction实现
        }
        yield return null;
    }

    //-----------------------------敌人回合后(玩家回合)--------------------------------
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        var heroView = HeroSystem.Instance.HeroView;

        int burnStacks = heroView.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, heroView);
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
        //heroView.UpdateEffectStacks();
        UpdateEffectGA updateEffectGA = new(heroView);
        ActionSystem.Instance.AddReaction(updateEffectGA);
    }


    //与事件有关的GA Performer(直接执行)
    private IEnumerator UpdateEffectPerformer(UpdateEffectGA updateEffectGA)
    {
        Debug.Log($"{updateEffectGA.CombatantView.name}");
        updateEffectGA.CombatantView.UpdateEffectStacks();   
        yield return null;
    }
}
