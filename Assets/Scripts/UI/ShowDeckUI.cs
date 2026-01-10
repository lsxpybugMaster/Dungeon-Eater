using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//与DeckUI类似的展示当前牌组的UI
public class ShowDeckUI : MonoBehaviour
{
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject cardUIRoot; //UI放置位置

    [SerializeField] private DeleteCardUI deleteCardUI;
    [SerializeField] private UpdateCardUI updateCardUI;

    private void OnEnable()
    {
        Debug.Log("Regist");
        deleteCardUI.OnCardUIDeleted += Show;
        updateCardUI.OnCardUIUpdated += Show;
    }

    private void OnDisable()
    {
        Debug.Log("UnRegist");
        deleteCardUI.OnCardUIDeleted -= Show;
        updateCardUI.OnCardUIUpdated -= Show;
    }

    //显示当前所有卡牌
    public void Show()
    {
        //既然已经使用了单例,就不再传入依赖了,不如直接获取
        var cardPile = GameManager.Instance.HeroState.Deck;

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
        CardUI cardUIInst = Instantiate(cardUIPrefab).GetComponent<CardUI>();
        //挂载到指定父节点下管理
        cardUIInst.transform.SetParent(cardUIRoot.transform);
        cardUIInst.Setup(card);
        //绑定其至显示UI
        deleteCardUI.RegistCardUI(cardUIInst);
        updateCardUI.RegistCardUI(cardUIInst);
    }
}
