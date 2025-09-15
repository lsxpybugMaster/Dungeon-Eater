using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化系统(如开局发牌）
/// </summary>
public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<CardData> deckData;

    void Start()
    {
        CardSystem.Instance.Setup(deckData);

        //第一次抽牌不需要作为回合结束的反应,直接执行
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

}
