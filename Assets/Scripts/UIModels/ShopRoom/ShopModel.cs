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
    protected readonly Func<int, bool> checkEnough; //判断钱币是否足够
    protected readonly Action<int> spendCoins;      //消耗钱币

    public List<ShopItem<TData>> shopItems { get; } = new();
    protected int shopItemsCount;

    public ShopModel(int shopItemsCount, Func<int, bool> checkEnough, Action<int> spendCoins)
    {
        this.shopItemsCount = shopItemsCount;
        this.checkEnough = checkEnough;
        this.spendCoins = spendCoins;
        // GenerateItems();
    }

    //Model 向 View 提供函数, 将要展示的数据传递给 View, 由 View 进行具体的展示
    public List<TData> GetDataForView()
    {
        List<TData> datas = new();
        for (int i = 0; i < shopItemsCount; i++)
        {
            datas.Add(shopItems[i].data);
        }
        return datas;
    }

    //View 向 Model 调用此函数, 生成道具, 生成和请求获取分离
    public virtual void GenerateItems()
    {
        Debug.Log("FOOD ITEM GEN BASE");
        shopItems.Clear();
    }

    //View 向 Model 调用, 以具体获取该商品
    public virtual void GainItemLogic(TData data)
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
