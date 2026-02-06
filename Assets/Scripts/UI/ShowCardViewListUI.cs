using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//与DeckUI类似的展示当前牌组的UI
public class ShowCardViewListUI : MonoBehaviour
{
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject cardUIRoot; //UI放置位置

    [SerializeField] private ShowCardUIBase showCardUI;

    private void OnEnable()
    {
        //showCardUI 可能不会被商店房使用
        if (showCardUI)
            showCardUI.OnCardUIClicked += Show;
    }

    private void OnDisable()
    {
        if (showCardUI)
            showCardUI.OnCardUIClicked -= Show;
    }


    public void Show()
    {
        //既然已经使用了单例,就不再传入依赖了,不如直接获取
        List<Card> cardPile = GameManager.Instance.HeroState.Deck;
        Show(cardPile, isGroup : false);
    }


    //显示当前所有卡牌
    public void Show(List<Card> cardPile, bool isGroup)
    {
        //删除已有的预制体
        for (int i = cardUIRoot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(cardUIRoot.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < cardPile.Count; i++)
        {
            Card card = cardPile[i];
            // idx = -1 时, 说明无需绑定 id 号
            int id = isGroup ? i : -1;
            InstantiateCardUIPrefab(card, idx : id);
        }
    }


    private void InstantiateCardUIPrefab(Card card, int idx)
    {
        CardUI cardUIInst = Instantiate(cardUIPrefab).GetComponent<CardUI>();
        //挂载到指定父节点下管理
        cardUIInst.transform.SetParent(cardUIRoot.transform);

        //这些 UIPrefab 是否属于一整组?
        if (idx >= 0) 
            cardUIInst.SetupForGroup(card, idx);
        else
            cardUIInst.Setup(card);

        //绑定其至显示UI
        //商店房的绑定UI

        //卡牌升级/删除的绑定UI
        showCardUI?.RegistCardUI(cardUIInst);
    }


}
