using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跨场景持久化的数据, 最小化维护开销
/// 即实际上的玩家全局数据
/// </summary>
[System.Serializable]
public class HeroState
{
    //DISCUSS: 为了完全封装HeroData,我们使用HeroState管理HeroData的不变数据部分,确保其他系统仅知道HeroData存在
    public HeroData BaseData {  get; private set; }

    //------------------------数据更新事件---------------------------
    public event Action<int> OnDeckSizeChanged;

    //------------------------动态数据---------------------------
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    /// <summary>
    /// 玩家局外卡组信息在此
    /// </summary>
    public List<CardData> Deck { get; private set; }

    //------------------------持久化数据---------------------------
    public Sprite HeroSprite => BaseData.Image;

    public int DeckSize => Deck.Count;


    //仅初始化一次初始数据
    public void Init(HeroData heroData)
    {
        BaseData = heroData;

        MaxHealth = heroData.Health;
        CurrentHealth = heroData.Health;
        Deck = heroData.Deck;
    }

    //及时接受更新的临时数据
    public void Save(int currentHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }


    /// <summary>
    /// 向牌组添加卡牌
    /// </summary>
    public void AddCardToDeck(CardData cardData)
    {
        OnDeckSizeChanged?.Invoke(DeckSize);
    }


    /// <summary>
    /// 向牌组删除卡牌
    /// </summary>
    public void RemoveCardFromDeck(CardData cardData)
    {
        OnDeckSizeChanged?.Invoke(DeckSize);
    }

}
