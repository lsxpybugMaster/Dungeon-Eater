using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ע��GameActionҪ�����Ӧ������System��ע��
/// </summary>
public class PlayCardGA : GameAction
{
    public Card Card { get; set; }

    public PlayCardGA(Card card)
    {
        Card = card;
    }
}

