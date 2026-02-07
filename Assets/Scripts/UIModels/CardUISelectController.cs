using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 目前只是一个接口标记, 后续考虑优化
/// 交互功能由CardUI管理,该系统只负责注册函数,解析信息,传递给上层
/// </summary>
public class CardUISelectController
{
    //只提供一个注册函数
    private Action<Card, int> onCardSelected;

    public CardUISelectController(Action<Card, int> onCardSelected) 
    {
        this.onCardSelected = onCardSelected;
    }

    //注册事件
    public void RegistCardUI(CardUI cardUI)
    {
        cardUI.OnCardSelectedInGroup += OnCardSelected;
    }


    //包装传入的函数, 转发一次信息
    public void OnCardSelected(Card card, int idx)
    {
        this.onCardSelected(card, idx);
    }
}
