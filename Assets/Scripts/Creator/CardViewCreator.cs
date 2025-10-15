using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    //对卡牌大小进行缩放
    [SerializeField] [Range(0,2f)] private float cardSize;
    //是CardView而非GameObject
    [SerializeField] private CardView cardViewPrefab;

    /// <summary>
    /// 生成卡牌视图
    /// </summary>
    /// <param name="card"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public CardView CreateCardView(Card card, Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        
        //生成卡牌时的放大效果
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one * cardSize , 0.15f);
        //初始化卡牌显示类的相关数据
        cardView.Setup(card);
        return cardView;
    }
}
