using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller将作为纯C#脚本管理数据或逻辑,由GameManager统一实例化与管理
/// </summary>
public class PlayerDeckController
{
    //由GameManager负责初始化
    private HeroState heroState;


    public int DeckSize => heroState.DeckSize;

    public PlayerDeckController(HeroState heroState)
    {
        this.heroState = heroState;
    }

    /*
        以后增删牌组的后续逻辑写在这里,HeroState仅负责最基本的数据
     */
    public void AddCardToDeck(CardData cardData)
    {
        heroState.AddCardToDeck(cardData);
        DoWhenUpdateCard(); 
    }

    /*
        升级卡牌: 删除卡牌后用新一级的卡牌替换
     */
    public void UpdateCardFromDeck(Card oriCard, CardData updatedCardData)
    {
        heroState.RemoveCardFromDeck(oriCard);
        //卡牌总数没变,不必更新UI
        heroState.AddCardToDeck(updatedCardData);
    }


    //IDEA: 考虑到可能对手牌进行更改,我们传入需要删除的卡牌实例进行删除
    public void RemoveCardFromDeck(Card card)
    {
        heroState.RemoveCardFromDeck(card);
        DoWhenUpdateCard();
    }

    private void DoWhenUpdateCard()
    {
        GameManager.Instance.PersistUIController.TopUI.UpdateDeckSize(heroState.DeckSize);
    }
}
