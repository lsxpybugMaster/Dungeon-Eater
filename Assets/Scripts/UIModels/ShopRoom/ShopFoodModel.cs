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
        
    }
}
