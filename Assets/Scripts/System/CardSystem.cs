using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    [SerializeField] private HandView handView;

    private readonly List<Card> drawPile = new();

    private readonly List<Card> discardPile = new();

    private readonly List<Card> hand = new();

    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    //注册及取消注册Performer与Reaction
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
    }


    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
    }


    //Performers
    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        //抽牌数量不能超过手牌数
        int actualAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);
        //还未抽到的牌数,如果不为0,后面需要洗牌
        int notDrawnAmount = drawCardsGA.Amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }
        
        if(notDrawnAmount > 0)
        {
            RefillDeck();
            for(int i = 0; i < notDrawnAmount ; i++)
            {
                yield return DrawCard();
            }
        }

    }

    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach (var card in hand)
        {
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    
    private IEnumerator DrawCard()
    {
        //ListExtensions中的List扩展方法
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }
     

    private void RefillDeck()
    {
        //把discardPile中的卡牌加到drawPile末尾
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }


    /// <summary>
    /// 对传入的cardView(即游戏中卡牌)进行相关改变并最终删除
    /// </summary>
    /// <param name="cardView"></param>
    /// <returns></returns>
    private IEnumerator DiscardCard(CardView cardView)
    {
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(drawPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
}
