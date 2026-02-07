using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public CardData Card;
    public int Price;
    public bool IsSold;

    public ShopItem(CardData card, int price, bool isSold = false)
    {
        Card = card;
        Price = price;
        IsSold = isSold;
    }
}


//商店 UIView 对应的 Model
public class ShopModel
{
    private HeroState heroState;

    public List<ShopItem> Items { get; } = new();
    private int shopCardsCount; //商店房展示多少商品

    public ShopModel(int shopCount) 
    {
        shopCardsCount = shopCount;
        heroState = GameManager.Instance.HeroState;
        GenerateCards();
    }

    public void GenerateCards()
    {
        Items.Clear();
        for (int i = 0; i < shopCardsCount; i++)
        {
            CardData d = CardDatabase.GetRandomCard();
            ShopItem item = new ShopItem(d, 10);
            Items.Add(item);
        }
    }

    public bool TryBuyCard(int id)
    {
        ShopItem buyItem = Items[id];
        int price = buyItem.Price;
        //钱不够直接返回
        if (!heroState.CheckCoinsEnough(price))
            return false;
              
        //钱够了则更新信息
        //更新商品信息
        buyItem.IsSold = true;
        heroState.SpendCoins(price);
        
        return true;
    }
}
