using System.Collections;
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


    void Start()
    {
        //FIXME: HACK方法,防止Battle场景初次进入时出错
        if (GameManager.Instance.HeroState.CurrentHealth == 0)
        {
            Debug.LogWarning("GameManager.Instance.HeroState is not init NOW!");        
        }
        else
        {
            SetupBattle();
        }
    }

    //IMPORTANT: 整个战斗场景的初始化入口:
    /* 持久化数据初始化
     * 不变数据初始化
     */
    private void SetupBattle()
    {
        Debug.Log("SETUPBATTLE");
        //传入了由GameManager维护的持久化数据
        HeroSystem.Instance.Setup(GameManager.Instance.HeroState, heroData);

        //初始化敌人信息
        EnemySystem.Instance.Setup(enemyDatas);

        CardSystem.Instance.Setup(heroData.Deck);

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
