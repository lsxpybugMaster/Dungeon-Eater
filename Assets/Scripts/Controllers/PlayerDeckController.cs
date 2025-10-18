using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller将作为纯C#脚本管理数据或逻辑,由GameManager统一实例化与管理
/// </summary>
public class PlayerDeckController
{
    //由GameManager负责初始化
    private HeroState heroState;

    //------------------------数据更新事件---------------------------
    public event Action<int> OnDeckSizeChanged;


    public PlayerDeckController(HeroState heroState)
    {
        this.heroState = heroState;
    }

    /*
        以后增删牌组的后续逻辑写在这里,HeroState仅负责最基本的数据
     */
    public void AddCardToDeck(CardData cardData)
    {
        heroState.AddCardToDeck(cardData);
        OnDeckSizeChanged?.Invoke(heroState.DeckSize);
    }


    public void RemoveCardFromDeck(CardData cardData)
    {
        heroState.RemoveCardFromDeck(cardData);
        OnDeckSizeChanged?.Invoke(heroState.DeckSize);
    }
}
