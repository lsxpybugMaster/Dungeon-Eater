using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem<TData>
{
    public TData data;
    public int Price; //可能与data.basePrice不同
    public bool IsSold;

    public ShopItem(TData data, int price, bool isSold = false)
    {
        this.data = data;
        Price = price;
        IsSold = isSold;
    }
}

//ShopUI(View) 对应的 Model
public class ShopModel<TData>
{
    //从外部注入与金钱判断有关的函数
    private readonly Func<int, bool> checkEnough; //判断钱币是否足够
    private readonly Action<int> spendCoins;      //消耗钱币

    public List<ShopItem<TData>> shopItems { get; } = new();
    private int shopItemsCount;

    public ShopModel(int shopItemsCount, Func<int, bool> checkEnough, Action<int> spendCoins)
    {
        this.shopItemsCount = shopItemsCount;
        this.checkEnough = checkEnough;
        this.spendCoins = spendCoins;
        GenerateItems();
    }

    public virtual void GenerateItems()
    {
    }

    public bool TryBuyItem(int id)
    {
        ShopItem<TData> buyItem = shopItems[id];
        int price = buyItem.Price;
        //钱不够直接返回
        if (!checkEnough(price))
            return false;
        //钱够了则更新信息
        //更新商品信息
        buyItem.IsSold = true;
        spendCoins(price);
        return true;
    }
}
