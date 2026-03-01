using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoneyGA : GameAction
{
    public int Amount { get; set; }
    public GetMoneyGA(int moneyAmount)
    {
        Amount = moneyAmount;
    }
}

