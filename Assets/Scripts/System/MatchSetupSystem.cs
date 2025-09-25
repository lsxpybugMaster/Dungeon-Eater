using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化系统(如开局发牌）
/// </summary>
public class MatchSetupSystem : MonoBehaviour
{
    /// <summary>
    /// 包含英雄的初始卡组等信息
    /// </summary>
    [SerializeField] private HeroData heroData;

    [SerializeField] private List<EnemyData> enemyDatas;

    void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        //初始化敌人信息
        EnemySystem.Instance.Setup(enemyDatas);

        CardSystem.Instance.Setup(heroData.Deck);

        ManaSystem.Instance.Setup();

        //第一次抽牌不需要作为回合结束的反应,直接执行
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

}
