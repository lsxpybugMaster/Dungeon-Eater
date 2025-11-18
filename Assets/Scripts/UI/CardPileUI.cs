using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示抽牌堆和弃牌堆数量
/// //NOTE: 后期需要支持牌堆信息渲染
/// </summary>
public class CardPileUI : MonoBehaviour
{
    [SerializeField] private TMP_Text disCardPileCountTMP;
    [SerializeField] private TMP_Text drawPileCountTMP;

    [SerializeField] private Button disCardPileBtn;
    [SerializeField] private Button drawPileBtn;

    private void Start()
    {
        //绑定抽牌堆弃牌堆的显示信息按钮
        var deckUI = GameManager.Instance.PersistUIController.DeckUI;
        disCardPileBtn.onClick.AddListener(() =>
        {
            deckUI.MoveUIWithLogic(CardSystem.Instance.DiscardPile);
        });
        drawPileBtn.onClick.AddListener(() =>
        {
            deckUI.MoveUIWithLogic(CardSystem.Instance.DrawPile);
        });
    }

    public void UpdataPileUI(int drawCount, int discardCount)
    {
        drawPileCountTMP.text = drawCount.ToString();
        disCardPileCountTMP.text = discardCount.ToString();
    }
}
