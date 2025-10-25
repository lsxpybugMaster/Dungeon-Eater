using ActionSystemTest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跨场景持久化的数据, 最小化维护开销
/// 即实际上的玩家全局数据
/// 尽可能保证仅含数据部分, 数据操作部分交给GameManager
/// </summary>
public class HeroState : BaseState<HeroData>
{
    //DISCUSS: 为了完全封装HeroData,我们使用HeroState管理HeroData的不变数据部分,确保其他系统仅知道HeroState存在
    //OPTIMIZE: 现在使用泛型抽象基类 BaseState 负责管理数据初始化以及以后可能的重复部分

    //------------------------动态数据---------------------------
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    /// <summary>
    /// 玩家局外卡组信息在此
    /// </summary>
    public List<Card> Deck { get; private set; }

    //------------------------持久化数据---------------------------
    public Sprite HeroSprite => BaseData.Image;

    public int DeckSize => Deck.Count;


    //仅初始化一次初始数据
    public HeroState()
    {
        //直接获取数据,不再通过Gamemanager传入
        //BaseData = Resources.Load<HeroData>();
        
        //继承基类后, 可以直接传入数据位置进行初始化
        LoadDataFromResources("HerosData/Knight");

        MaxHealth = BaseData.Health;
        CurrentHealth = BaseData.Health;

        //别忘记初始化!
        Deck = new List<Card>();

        //DISCUSS: 我们实际上应当保管Card作为动态数据,否则会修改CardData
        foreach (var cardData in BaseData.Deck)
        {
            Card card = new(cardData);
            Deck.Add(card);
        }

        //IMPORTANT: C# 中 '=' 永远是浅拷贝
        /* 
         * 注意需要深拷贝!
         * Deck = new List<CardData>(heroData.Deck);
         * 这是浅拷贝! Deck修改heroData.Deck也会被修改
         * Deck = heroData.Deck;
        */
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
        DebugUtil.Magenta("Add Card!");
        Card card = new(cardData);
        Deck.Add(card);
    }


    /// <summary>
    /// 向牌组删除卡牌
    /// </summary>
    public void RemoveCardFromDeck(Card card)
    {
        if (Deck.Contains(card))
        {
            Deck.Remove(card);
        }
        else Debug.LogError("未发现应该删除的卡牌");
    }

}
