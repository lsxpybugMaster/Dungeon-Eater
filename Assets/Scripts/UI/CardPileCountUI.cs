using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 显示抽牌堆和弃牌堆数量
/// //NOTE: 后期需要支持牌堆信息渲染
/// </summary>
public class CardPileCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text disCardPileCountTMP;
    [SerializeField] private TMP_Text drawPileCountTMP;

    public void UpdataPileUI(int drawCount, int discardCount)
    {
        drawPileCountTMP.text = drawCount.ToString();
        disCardPileCountTMP.text = discardCount.ToString();
    }
}
