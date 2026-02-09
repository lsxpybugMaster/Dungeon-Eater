using System.Collections;
using System.Collections.Generic;
using UIUtils;
using UnityEngine;


public class PriceUI : UWithText
{
    public int price { get; set; }
    //动态显示价格颜色
    public Color enoughMoneyColor;
    public Color notEnoughMoneyColor;

    //与玩家金币绑定,在金币不够时做特殊显示
    private new void Awake()
    {
        GameManager.Instance.HeroState.OnCoinChange += SetPriceView;  
    }

    //初始化PriceUI信息
    public void Init(int price)
    {
        this.price = price;
        SetPriceView(GameManager.Instance.HeroState.Coins);
    }

    //玩家金币变化时更新
    private void SetPriceView(int m)
    {
        T = price + "G";
        Color c = m >= price? enoughMoneyColor: notEnoughMoneyColor;
        tmp_text.color = c;
    }
}
