using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注意GameAction要在其对应所属的System中注册
/// </summary>
public class PlayCardGA : GameAction
{
    public Card Card { get; set; }

    public PlayCardGA(Card card)
    {
        Card = card;
    }
}

