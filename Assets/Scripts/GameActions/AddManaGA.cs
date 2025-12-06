using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddManaGA : GameAction {
    public int Amount { get; private set; }
    public bool Refill { get; private set; } = false;
    public AddManaGA(int amount, bool refill)
    {
        Amount = amount;
        Refill = refill;
    }
}
