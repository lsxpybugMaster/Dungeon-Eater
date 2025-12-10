using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PileType
{
    DrawPile,
    DisCardPile,
    HandPile,
}

/*
 //TODO: 优化该模块:
    提取PileSystem维护牌堆: 避免手动调用  
        OnPileChanged?.Invoke(DrawPileCount, DiscardPileCount);
        DoWhenDeckChanged();
 */
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
    [SerializeField] private Transform showNewCardPoint;
    
    //NOTE: 每当牌堆改变时与UI交互
    public event Action<int, int> OnPileChanged; //上面这个仅限抽牌堆弃牌堆

    // 只读属性,供UI显示信息
    public int DrawPileCount => drawPile.Count;
    public int DiscardPileCount => discardPile.Count;

    public int DeckCount => drawPile.Count + discardPile.Count + hand.Count;
    //后期可能需要使用
    public IReadOnlyList<Card> DrawPile => drawPile;
    public IReadOnlyList<Card> DiscardPile => discardPile;
    public IReadOnlyList<Card> HandPile => hand;

    /// <summary>
    /// 初始化牌堆
    /// </summary>
    /// <param name="deckData">传入卡牌数据列表</param>
    //IMPORTANT: 我们管理的是Card!! 不要使用CardData
    public void Setup(List<Card> deckData)
    {
        //初始填满抽牌堆
        foreach (var herocard in deckData)
        {
            Card card = new(herocard.data);
            drawPile.Add(card);
        }
    }

    //返回战斗模式下手牌
    public List<Card> GetDeck()
    {
        var deck = new List<Card>(DeckCount);
        deck.AddRange(drawPile);
        deck.AddRange(discardPile);
        deck.AddRange(hand);
        return deck;
    }


    /// <summary>
    /// 在局内增加卡牌至指定牌堆(与全局的卡牌堆不同)
    /// </summary>
    public void AddCardToPile(Card card, PileType pileType)
    {
        switch (pileType)
        {
            case PileType.DrawPile:
                drawPile.Add(card); break;
            case PileType.DisCardPile:
                discardPile.Add(card); break;
            case PileType.HandPile:
                hand.Add(card); break;
        }

        //及时更新,确保UI与逻辑同步
        OnPileChanged?.Invoke(DrawPileCount, DiscardPileCount);
        //TopUI也要同步
        DoWhenDeckChanged();
    }

    private void DoWhenDeckChanged()
    {
        GameManager.Instance.PersistUIController.TopUI.UpdateDeckSize(DeckCount);
    }

    /*
    //NOTE: Performer执行逻辑
       如果内部包含跨帧逻辑：
        +  yield 动画
        +  yield 子协程
        +  yield 回调等待
       如果内部没有跨帧：
        +  在最后添加 yield return null;
     */

    //注册及取消注册Performer与Reaction
    private void OnEnable()
    {
        //注册Performer,指明执行该行动的协程
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.AttachPerformer<AddCardGA>(AddCardPerformer);
    }


    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.DetachPerformer<AddCardGA>();
    }


    private IEnumerator AddCardPerformer(AddCardGA addCardGA)
    {
        var card   = addCardGA.WhichCard;
        var pile   = addCardGA.WhichPileToAdd;
        var caster = addCardGA.Caster;

        int drct = caster is HeroView ? 1 : -1; 

        //前摇动画
        yield return MotionUtil.Dash(
            caster.transform,
            new Vector2(drct * 1f, 0),
            Config.Instance.attackTime
        );


        //创建一个展示卡牌(仅动画效果)
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, 
            showNewCardPoint.position, 
            showNewCardPoint.rotation, 
            showMode: true
        );

        //展示卡牌的时间
        yield return new WaitForSeconds(Config.Instance.freezeTime);

        // 根据添加的位置(抽牌堆,弃牌堆,手牌堆) 确定效果类型
        if (pile == PileType.HandPile)
        {
            //继续减小卡牌大小(原来是显示级大小)
            cardView.transform.DOScale(Vector3.one * Config.Instance.cardSize , Config.Instance.moveTime);
            // 将卡牌添加到手牌
            yield return handView.AddCard(cardView);

            cardView.EnableCardInteraction();
        }
        else
        {
            //效果:卡牌变小并向对应的位置移动
            Vector3 pos = pile == PileType.DrawPile ? drawPilePoint.position : discardPilePoint.position;
            cardView.transform.DOScale(Vector3.zero, Config.Instance.moveTime);
            Tween tween = cardView.transform.DOMove(pos, Config.Instance.moveTime);

            yield return tween.WaitForCompletion();

            Destroy(cardView.gameObject);
        }

        //实际逻辑上的添加
        AddCardToPile(addCardGA.WhichCard, addCardGA.WhichPileToAdd);
    }


    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="drawCardsGA">注意该GameAction含有属性Amount</param>
    /// <returns></returns>
    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int amount = drawCardsGA.Amount;

        //从目前牌堆能抽出的最多数量
        int drawFromCurrentPileAmount = Mathf.Min(amount, drawPile.Count);
        //还未抽到的牌数,如果不为0,后面需要洗牌
        int notDrawnAmount = amount - drawFromCurrentPileAmount;
        //确保抽的牌数不会比总的手牌还多,避免空指针
        notDrawnAmount = Mathf.Min(notDrawnAmount, discardPile.Count);
        //实际抽的牌数为: drawFromCurrentPileAmount + notDrawnAmount

        //从抽牌堆抽牌
        for (int i = 0; i < drawFromCurrentPileAmount; i++)
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


    //IMPORTANT: 所有卡牌的功能在这里执行
    /// <summary>
    /// 卡牌功能执行函数
    /// </summary>
    /// <param name="playCardGA"></param>
    /// <returns></returns>
    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        //删除卡牌后卡牌位置更新,同时返回删除的卡片
        CardView cardView = handView.RemoveCard(playCardGA.Card);

        //TODO: 提取出这部分逻辑
        if (playCardGA.Card.HasTag(CardTag.Exhaust))
        {
            yield return ExhaustCard(cardView);
        }
        else
        {
            yield return DiscardCard(cardView);
        }


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
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets(playCardGA.ManualTarget);

            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            //注意现在是在Performer中,若想执行其他Action必须使用AddReaction 

            ActionSystem.Instance.AddReaction(performEffectGA);
        }
    }


    //注意上面的Performers不主动执行,而是作为reaction

    //在打出卡牌时删除卡牌,请与删除所选牌堆中卡牌区分
    private IEnumerator ExhaustCard(CardView cardView)
    {
        Tween tween = cardView.transform.DOScale(Vector3.zero, Config.Instance.moveTime);
        yield return tween.WaitForCompletion();

        Destroy(cardView.gameObject);

        OnPileChanged?.Invoke(DrawPileCount, DiscardPileCount);
        //TopUI也要同步
        DoWhenDeckChanged();
    }

    private IEnumerator DrawCard()
    {
        //ListExtensions中的List扩展方法
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);

        //NOTE: 激活事件,通知UI更新
        OnPileChanged?.Invoke(DrawPileCount, DiscardPileCount);
    }
     

    private void RefillDeck()
    {
        //把discardPile中的卡牌加到drawPile末尾
        drawPile.AddRange(discardPile);
        discardPile.Clear();

        OnPileChanged?.Invoke(DrawPileCount, DiscardPileCount);
    }



    /// <summary>
    /// 对传入的cardView(即游戏中卡牌)进行相关改变并最终删除
    /// </summary>
    /// <param name="cardView"></param>
    /// <returns></returns>
    private IEnumerator DiscardCard(CardView cardView)
    {
        //在这里弃牌,确保数据层与显示层一致
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, Config.Instance.moveTime);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, Config.Instance.moveTime);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);

        OnPileChanged?.Invoke(DrawPileCount, DiscardPileCount);
    }
}

