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
public class ShopModel : IamUIModel
{
    public List<ShopItem> Items { get; } = new();
    private int shopCardsCount; //商店房展示多少商品

    public ShopModel(int shopCount) 
    {
        shopCardsCount = shopCount;
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
}
