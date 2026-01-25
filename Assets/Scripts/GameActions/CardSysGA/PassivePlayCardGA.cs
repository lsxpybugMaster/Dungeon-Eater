using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePlayCardGA : GameAction
{
    // public EnemyView ManualTarget { get; private set; }
    public Card Card { get; set; }

    public PassivePlayCardGA(Card card)
    {
        Card = card;
        // ManualTarget = null;
    }
}
