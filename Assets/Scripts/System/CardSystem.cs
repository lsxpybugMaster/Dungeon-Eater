using ActionSystemTest;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;

    private readonly List<Card> drawPile = new();

    private readonly List<Card> discardPile = new();
    /// <summary>
    /// 手牌牌堆
    /// </summary>
    private readonly List<Card> hand = new();

    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    //注册及取消注册Performer与Reaction
    private void OnEnable()
    {
        //注册Performer,指明执行该行动的协程
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        
    }


    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
    }


    /// <summary>
    /// 初始化牌堆
    /// </summary>
    /// <param name="deckData">传入卡牌数据列表</param>
    public void Setup(List<CardData> deckData)
    {
        foreach (var cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
    }


    //Performers

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="drawCardsGA">注意该GameAction含有属性Amount</param>
    /// <returns></returns>
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
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }


    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        //删除卡牌后卡牌位置更新,同时返回删除的卡片
        CardView cardView = handView.RemoveCard(playCardGA.Card);
        yield return DiscardCard(cardView);

        //减少对应的法力值
        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);

        //解析该卡牌的手动指示目标Effect
        if (playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectGA performEffectGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        //注意手动指示目标Effect和其余Effect不冲突,因此这里无需if-else

        //解析该卡牌的其余Effect并"执行"(因为不会立刻执行)
        foreach (var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();

            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            //注意现在是在Performer中,若想执行其他Action必须使用AddReaction 

            ActionSystem.Instance.AddReaction(performEffectGA);
        }
    }


    //注意上面的Performers不主动执行,而是作为reaction



    private IEnumerator DrawCard()
    {
        //Debug.Log($"Draw BEFORE: draw:{drawPile.Count}, discard:{discardPile.Count}, hand:{hand.Count}");

        //ListExtensions中的List扩展方法
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }
     

    private void RefillDeck()
    {
        //Debug.Log($"Refill: move {discardPile.Count} cards to drawPile");

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
        //Debug.Log($"Discard AFTER: draw:{drawPile.Count}, discard:{discardPile.Count}, hand:{hand.Count}");

        //在这里弃牌,确保数据层与显示层一致
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
}
