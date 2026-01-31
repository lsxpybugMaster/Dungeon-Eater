using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendManaGA : GameAction
{
    public int Amount { get; set; }

    public SpendManaGA(int amount)
    {
        Amount = amount;
    }
}

public class SpendOtherManaGA : GameAction
{
    public int Amount { get; set; }
    public ManaID ManaType { get; set; }

    public SpendOtherManaGA(int amount, ManaID manaType)
    {
        Amount = amount;
        ManaType = manaType;
    }
}

