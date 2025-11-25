using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardGA : GameAction, IHaveCaster //区分玩家(或者敌人)添加的卡牌
{
    public CombatantView Caster { get; private set; }

    public PileType whichPileToAdd { get; private set; }

    public Card whichCard { get; private set; }

    public AddCardGA(PileType pileType, CombatantView caster, CardData cardData)
    {
        Caster = caster;
        whichPileToAdd = pileType;
        //根据外部传入的数据进行初始化Card
        whichCard = new Card(cardData);
    }
}
