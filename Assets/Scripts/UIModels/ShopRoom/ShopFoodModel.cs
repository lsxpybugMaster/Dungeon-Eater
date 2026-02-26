using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFoodModel : ShopModel<FoodData>
{
    public ShopFoodModel(int shopItemsCount, Func<int, bool> checkEnough, Action<int> spendCoins) : base(shopItemsCount, checkEnough, spendCoins)
    {
    }

    /// <summary>
    /// 决定如何生成商店对应的商品
    /// </summary>
    public override void GenerateItems()
    {
        base.GenerateItems();
        for (int i = 0; i < shopItemsCount; i++)
        {
            FoodData foodData = FoodDataBase.GetRandomFood();
            var item = new ShopItem<FoodData>(foodData, foodData.BasePrice);
            shopItems.Add(item);
        }
    }

    public override void GainItemLogic(FoodData data)
    {
        GameManager.Instance.PlayerFoodController.AddToPlayer(data);
    }
}
