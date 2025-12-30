using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//TODO: 重构逻辑
//IDEA: 将其也作为跨场景UI(因为调用其show函数的按钮在GlobalUI上)
public class DeckUI : MonoBehaviour, IUIMove<IReadOnlyList<Card>>, IAmPersistUI
{
    //NOTE: 组合优于继承,该UI能够移动
    private UIMoveComponent uiMoveComponent;

    //IMPORTANT: 预制体一定要先Instantiate!!
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject cardUIRoot; //UI放置位置

    public void Setup()
    {
        
    }

    private void Awake()
    {
        uiMoveComponent = GetComponentInChildren<UIMoveComponent>();
        if (uiMoveComponent == null)
            Debug.LogError("未发现UIMoveComponent组件!");
    }

    private void Update()
    {
    }


    public void MoveUIWithLogic(IReadOnlyList<Card> cards)
    {
        //说明要进入,更新UI
        if (uiMoveComponent.isInOriginPos)
        {
            Show(cards);
        }
        uiMoveComponent.SwitchModeMoveUI();
    }

    /// <summary>
    /// 根据外部传入的卡牌数据展示对应卡牌
    /// </summary>
    private void InstantiateCardUIPrefab(Card card)
    {
        GameObject mygo = Instantiate(cardUIPrefab);
        //挂载到指定父节点下管理
        mygo.transform.SetParent(cardUIRoot.transform);
        mygo.GetComponent<CardUI>().Setup(card);
    }

    /// <summary>
    /// 由外部引用
    /// </summary>
    /// <param name="cardPile"></param>
    private void Show(IReadOnlyList<Card> cardPile)
    {
        //TODO: 优化这部分逻辑
        //如果没有指明牌堆,那么根据游戏模式确定展示全局牌堆还是战斗牌堆
        if (cardPile == null)
        {
            GameState gameState = GameManager.Instance.GameState;
            if(gameState == GameState.Battle || gameState == GameState.BattleVictory)
            {
                cardPile = CardSystem.Instance.GetDeck();
            }
            else
            {
                cardPile = GameManager.Instance.HeroState.Deck;          
            }
        }
        //否则说明指定了牌堆

        //TODO: 使用对象池等修改逻辑
        //预先清空之前的预制体
        for (int i = cardUIRoot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(cardUIRoot.transform.GetChild(i).gameObject);
        }

        // List<Card> cards = GameManager.Instance.HeroState.Deck;
        foreach (var card in cardPile)
        {
            InstantiateCardUIPrefab(card);
        }
    }
}
