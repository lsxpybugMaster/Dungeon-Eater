using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 给玩家牌组里塞点好东西
/// </summary>
public class AddCardIntend : EnemyIntend
{
    [SerializeField] private List<CardData> cardData;
    [SerializeField] private PileType pileType;

    public override GameAction GetGameAction(EnemyView enemy)
    {
        AddCardGA addCardGA = new(pileType, enemy, cardData);
        return addCardGA;
    }
}
