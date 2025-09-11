using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    //专门用于展示卡牌信息的卡牌
    [SerializeField] private CardView cardViewHover;

    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        //根据数据更新卡牌信息
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position;
    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }
}
