﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化系统(如开局发牌）
/// 现在还管理整个关卡
/// </summary>
// 原MatchSetupSystem
public class BattleControlSystem : MonoBehaviour
{
    /// <summary>
    /// 包含英雄的初始卡组等信息
    /// </summary>
    [SerializeField] private HeroData heroData;

    [SerializeField] private List<EnemyData> enemyDatas;

    [SerializeField] private PerkData perkData;

    private void OnEnable()
    {
        GameManager.OnGameManagerInitialized += SetupBattle;
        //监听事件,一旦所有敌人被杀死,则执行胜利结算的相关工作
        ActionSystem.SubscribeReaction<KillAllEnemyGA>(BattleWin, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerInitialized -= SetupBattle;

        ActionSystem.UnsubscribeReaction<KillAllEnemyGA>(BattleWin, ReactionTiming.POST);
    }

    private bool hasSetup = false;

    void Start()
    {
        /*
            问题:
            一般游戏时先进入title,此时GameManger实例化,并按序初始化Systems
            然而如果直接进入Battle,可能由于脚本执行顺序问题,BattleSystem提早初始化,然后GameManger实例化后出现了:
          //BUG: 重复初始化问题
         */

        //防止初次进入Battle场景时该脚本早于GameManager初始化
        if (!hasSetup) 
            SetupBattle();
    }

    //IMPORTANT: 整个战斗场景的初始化入口:
    /* 持久化数据初始化
     * 不变数据初始化
     */
    private void SetupBattle()
    {
        Debug.Log("SETUPBATTLE");
        hasSetup = true; 

        HeroState heroState = GameManager.Instance.HeroState;

        //OPTIMIZE: 现在HeroState完全封装了HeroData!!
        HeroSystem.Instance.Setup(heroState);

        //初始化敌人信息
        EnemySystem.Instance.Setup(enemyDatas);

        //现在要传入持久化数据了
        CardSystem.Instance.Setup(heroState.Deck);

        // 这部分是持久化数据,目前不应放在该位置
        // 初始化天赋
        // PerkSystem.Instance.AddPerk(new Perk(perkData));

        ManaSystem.Instance.Setup();

        //第一次抽牌不需要作为回合结束的反应,直接执行
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

    /// <summary>
    /// 玩家获得胜利后的逻辑处理
    /// </summary>
    private void BattleWin(KillAllEnemyGA killAllEnemyGA)
    {
        UISystem.Instance.ShowWinUI();
        // 显示完UI更换游戏模式，以禁用输入 
        GameManager.Instance.ChangeGameState(GameState.BattleVictory);
    }
}
