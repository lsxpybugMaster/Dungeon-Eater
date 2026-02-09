using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : RoomUI
{
    [SerializeField] private int shopCardCount;
    //展示出售的卡牌
    [SerializeField] private ShowCardViewListUI showShopCardUI;
    
    private ShopModel shopModel;

    private CardUISelectController cardUISelectController;
    
    private new void Awake()
    {
        base.Awake();
        shopModel = new(shopCardCount);
        cardUISelectController = new((Card c, int id) => BuyCard(c, id));
        showShopCardUI.CardSelectedController = cardUISelectController;
    }


    //每次进入时都要重新生成商店的商品
    protected override void OnShow()
    {
        base.OnShow();
        showShopCardUI.Show(GetCardDataList(shopModel), isGroup : true);
        ShowCardPrice();
    }

    //显示卡牌的价格
    private void ShowCardPrice()
    {
        List<CardUI> cardUIs = showShopCardUI.cardUIs;
        List<ShopItem> shopItems = shopModel.Items;
        for (int i = 0; i < cardUIs.Count; i++)
        {
            PriceUI priceUI = cardUIs[i].gameObject.GetComponent<PriceUI>();
            priceUI.Init(shopItems[i].Price);
        }
    }

    //UI将逻辑交付给 Model 处理,自己只做卡牌 View 的对应修改
    private void BuyCard(Card card, int id)
    {
        CardUI cardUI = showShopCardUI.cardUIs[id];

        if (!shopModel.TryBuyCard(id))
            return;
        //Model层面购买成功, View层开始展示

        //首先取消交互
        cardUI.DisableCasting();
        AnimStatic.CardScaleAnim(cardUI, Vector3.zero);
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
