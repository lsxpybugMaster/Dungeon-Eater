using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化系统(如开局发牌）
/// 现在还管理整个关卡
/// </summary>
public class BattleControlSystem : MonoBehaviour
{
    /// <summary>
    /// 包含英雄的初始卡组等信息
    /// </summary>
    [SerializeField] private HeroData heroData;

    [SerializeField] private List<EnemyData> enemyDatas;

    [SerializeField] private PerkData perkData;
 
    void Start()
    {
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
    private void BattleWin()
    {

    }

    //切换场景时执行
    private void OnDestroy()
    {
        HeroSystem.Instance?.SaveData();
        //在这里切换游戏模式:
        GameManager.Instance?.ChangeGameState(GameState.Exploring);
    }
  
}
