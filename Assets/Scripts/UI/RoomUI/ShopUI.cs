using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : RoomUI
{
    [SerializeField] private int shopCardCount;
    [SerializeField] private int shopFoodCount;

    #region [ViewList] 展示商品列表
    //展示出售的卡牌
    [SerializeField] private ShowCardViewListUI showShopCardUI;

    [SerializeField] private ShowFoodListUI showFoodListUI;
    #endregion

    #region [Model] Model部分, 实际的道具生成在Model中
    private ShopCardModel shopModel;
    //private ShopModel<FoodData> shopFoodModel;
    //OPTIMIZE: 这里是否可以进一步依赖注入, 不使用新的子类？
    private ShopFoodModel shopFoodModel;

    #endregion

    private CardUISelectController cardUISelectController;
    
    private new void Awake()
    {
        base.Awake();
       
        HeroState heroState = GameManager.Instance.HeroState;
        shopFoodModel = new(
            shopFoodCount,
            heroState.CheckCoinsEnough,
            heroState.SpendCoins
        );

        shopModel = new(shopCardCount);
        cardUISelectController = new((Card c, int id) => BuyCard(c, id));
        showShopCardUI.CardSelectedController = cardUISelectController;
    }


    //每次进入时都要重新生成商店的商品
    protected override void OnShow()
    {
        base.OnShow();
        showShopCardUI.Show(GetCardDataList(shopModel), isGroup:true);

        //STEP: View 调用 Model 先生成道具
        shopFoodModel.GenerateItems();
        showFoodListUI.Show(shopFoodModel.GetDataForView(), isGroup:true);
        showFoodListUI.BindOnItemSelectedInGroup((FoodData d, int id) =>
        {
            Buy(d, id, showFoodListUI, shopFoodModel);
        });
        
        ShowCardPrice();

        ///dev
        ShowPrice(showFoodListUI.itemUIs);
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

    /// <summary>
    /// [通用函数]
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="itemUIs"></param>
    private void ShowPrice<TData>(List<ItemUI<TData>> itemUIs) where TData : IShopItem
    {
        for (int i = 0; i < itemUIs.Count; i++)
        {
            PriceUI priceUI = itemUIs[i].gameObject.GetComponent<PriceUI>();
            priceUI.Init(itemUIs[i].Data.BasePrice);
        }
    }

    /// <summary>
    /// 通用购买函数
    /// </summary>
    private void Buy<TData>(TData itemData, int id, ShowItemListUI<TData> listUI,
        ShopModel<TData> shopModel)
    {
        ItemUI<TData> itemUI = listUI.itemUIs[id];

        if (!shopModel.TryBuyItem(id))
            return;

        itemUI.DisableInteraction();
        AnimStatic.ItemScaleAnim(itemUI.transform, Vector3.zero);


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

        //购买后的商品获取逻辑:
        GameManager.Instance.PlayerDeckController.AddCardToDeck(card.data);
    }
    

    //生成新的商店卡牌
    private List<Card> GetCardDataList(ShopCardModel shopModel)
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
