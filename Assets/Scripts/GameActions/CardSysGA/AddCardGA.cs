using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardGA : GameAction, IHaveCaster //区分玩家(或者敌人)添加的卡牌
{
    public CombatantView Caster { get; private set; }

    public PileType WhichPileToAdd { get; private set; }

    //注意创建空字典时别忘记new!
    public List<Card> AddCardList { get; private set; } = new(); 

    public AddCardGA(PileType pileType, CombatantView caster, List<CardData> cardData)
    {
        Caster = caster;
        WhichPileToAdd = pileType;
        //根据外部传入的数据进行初始化Card列表
        foreach (CardData card in cardData)
        {
            AddCardList.Add(new Card(card));
        }
    }
}
