using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注意GameAction要在其对应所属的System中注册
/// </summary>
public class PlayCardGA : GameAction
{
    public EnemyView ManualTarget {  get; private set; }
    public Card Card { get; set; }

    public PlayCardGA(Card card)
    {
        Card = card;
        ManualTarget = null;
    }

    /// <summary>
    /// 该构造器用于声明带手动目标的卡牌技能
    /// </summary>
    /// <param name="card"></param>
    /// <param name="target"></param>
    public PlayCardGA(Card card, EnemyView target)
    {
        Card = card;
        ManualTarget = target;
    }
}

