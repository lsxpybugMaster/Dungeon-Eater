using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跨场景持久化的数据, 最小化维护开销
/// 即实际上的玩家全局数据
/// 尽可能保证仅含数据部分, 数据操作部分交给GameManager
/// </summary>
[System.Serializable]
public class HeroState
{
    //DISCUSS: 为了完全封装HeroData,我们使用HeroState管理HeroData的不变数据部分,确保其他系统仅知道HeroData存在
    public HeroData BaseData {  get; private set; }

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
    public HeroState(HeroData heroData)
    {
        BaseData = heroData;

        MaxHealth = heroData.Health;
        CurrentHealth = heroData.Health;
           
        //IMPORTANT: C# 中 '=' 永远是浅拷贝

        //注意需要深拷贝!
        Deck = new List<CardData>(heroData.Deck);
        //BUG: 这是浅拷贝!
        // Deck = heroData.Deck;
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
        Deck.Add(cardData);
    }


    /// <summary>
    /// 向牌组删除卡牌
    /// </summary>
    public void RemoveCardFromDeck(CardData cardData)
    {
        if (Deck.Contains(cardData))
        {
            Deck.Remove(cardData);
        }
        else Debug.LogError("未发现应该删除的卡牌");
    }

}
