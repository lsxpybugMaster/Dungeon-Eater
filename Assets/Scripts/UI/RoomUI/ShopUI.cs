using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : RoomUI
{
    [SerializeField] private int shopCardCount;
    //展示出售的卡牌
    [SerializeField] private ShowCardViewListUI showShopCardUI;
    private ShopModel shopModel;

    
    private new void Awake()
    {
        base.Awake();
        shopModel = new(shopCardCount);
    }


    //每次进入时都要重新生成商店的商品
    protected override void OnShow()
    {
        base.OnShow();
        showShopCardUI.Show(GetCardDataList(shopModel), isGroup : true);
    }


    //生成新的商店卡牌
    private List<Card> GetCardDataList(ShopModel shopModel)
    {
        shopModel.GenerateCards();
        List<ShopItem> shopItems = shopModel.Items;
        List<Card> cards = new List<Card>();
        foreach (ShopItem item in shopItems) 
        {
            cards.Add(new Card(item.Card));
        }
        return cards;
    }
}
