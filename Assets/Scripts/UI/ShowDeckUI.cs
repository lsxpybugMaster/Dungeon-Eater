using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//与DeckUI类似的展示当前牌组的UI
public class ShowDeckUI : MonoBehaviour
{
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject cardUIRoot; //UI放置位置

    //TODO: 现在暂时由其管理选中卡牌展示功能
    public CardUI choosenCardUI;


    public void ShowChoosenCard(Card card)
    {
        choosenCardUI.Setup(card);
        Debug.Log($"Show Choosen {card}");
    }

    //显示当前所有卡牌
    public void Show(IReadOnlyList<Card> cardPile)
    {
        //删除已有的预制体
        for (int i = cardUIRoot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(cardUIRoot.transform.GetChild(i).gameObject);
        }

        //添加最新的
        foreach (var card in cardPile)
        {
            InstantiateCardUIPrefab(card);
        }
    }

    private void InstantiateCardUIPrefab(Card card)
    {
        GameObject mygo = Instantiate(cardUIPrefab);
        //挂载到指定父节点下管理
        mygo.transform.SetParent(cardUIRoot.transform);
        mygo.GetComponent<CardUI>().Setup(card);

        mygo.GetComponent<CardUI>().OnCardChoosen = ShowChoosenCard;
    }
}
